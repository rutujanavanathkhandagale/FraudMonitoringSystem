using FraudMonitoringSystem.DTOs.Investigator;
using FraudMonitoringSystem.Exceptions.Investigator;
using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.Models.Rules;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Investigator;
using FraudMonitoringSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations.Investigator
{
    public class RiskScoreRepository : IRiskScoreRepository
    {
        private readonly WebContext _context;

        public RiskScoreRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<RiskScoreDto> EvaluateRiskScoreAsync(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction == null) throw new NotFoundException("Transaction not found");

            var rules = await _context.DetectionRules
                                      .Include(r => r.Scenario)
                                      .Where(r => r.IsActive || r.Status == "Active")
                                      .ToListAsync();

            if (!rules.Any()) throw new AppException("No active detection rules found");

            var reasons = new List<string>();
            decimal score = 0;

            foreach (var rule in rules)
            {
                bool isTriggered = false;
                string reason = string.Empty;

                if (rule.Expression.Contains("amount >", StringComparison.OrdinalIgnoreCase))
                {
                    if (transaction.Amount > rule.Threshold)
                    {
                        isTriggered = true;
                        score += 10;
                        reason = $"Rule '{rule.Scenario?.Name ?? rule.Name}': Transaction amount ({transaction.Amount}) exceeds {rule.Threshold}. (+10 Points)";
                    }
                }
                else if (rule.Expression.Contains("TransactionTime BETWEEN", StringComparison.OrdinalIgnoreCase))
                {
                    var hour = transaction.Timestamp.Hour;   // ✅ use Timestamp
                    if (hour >= 23 || hour < 5)
                    {
                        isTriggered = true;
                        score += 15;
                        reason = $"Rule '{rule.Scenario?.Name ?? rule.Name}': Transaction occurred at an odd hour ({transaction.Timestamp:HH:mm}). (+15 Points)";
                    }
                }
                else if (rule.Expression.Contains("Count(Transactions", StringComparison.OrdinalIgnoreCase))
                {
                    var fiveMinsAgo = transaction.Timestamp.AddMinutes(-5);
                    var recentTransactionsCount = await _context.Transactions
                        .CountAsync(t => t.CustomerId == transaction.CustomerId &&   // ✅ use CustomerId
                                         t.Timestamp >= fiveMinsAgo &&
                                         t.Timestamp <= transaction.Timestamp);

                    if (recentTransactionsCount > rule.Threshold)
                    {
                        isTriggered = true;
                        score += 25;
                        reason = $"Rule '{rule.Scenario?.Name ?? rule.Name}': Rapid velocity detected. (+25 Points)";
                    }
                }
                else if (rule.Expression.Contains("Country IN", StringComparison.OrdinalIgnoreCase))
                {
                    var highRiskLocations = new List<string> { "Russia", "Syria", "North Korea", "Iran" };
                    if (highRiskLocations.Contains(transaction.GeoLocation, StringComparer.OrdinalIgnoreCase))   // ✅ use GeoLocation
                    {
                        isTriggered = true;
                        score += 30;
                        reason = $"Rule '{rule.Scenario?.Name ?? rule.Name}': Transaction originated from a monitored location ({transaction.GeoLocation}). (+30 Points)";
                    }
                }

                if (isTriggered) reasons.Add(reason);
            }

            score = Math.Clamp(score, 0, 100);

            if (score == 0 || !reasons.Any())
            {
                reasons.Add("Transaction is safe: No high-risk rules were triggered. Score is 0.");
            }

            var existingScore = await _context.RiskScores.FirstOrDefaultAsync(rs => rs.TransactionID == transactionId);
            RiskScore riskScore;

            if (existingScore != null)
            {
                existingScore.ScoreValue = score;
                existingScore.ReasonsJSON = JsonSerializer.Serialize(reasons);
                existingScore.EvaluatedAt = DateTime.UtcNow;

                _context.RiskScores.Update(existingScore);
                riskScore = existingScore;
            }
            else
            {
                riskScore = new RiskScore
                {
                    ScoreId = Guid.NewGuid().ToString(),
                    TransactionID = transaction.TransactionID,
                    ScoreValue = score,
                    ReasonsJSON = JsonSerializer.Serialize(reasons),
                    EvaluatedAt = DateTime.UtcNow
                };

                _context.RiskScores.Add(riskScore);
            }

            await _context.SaveChangesAsync();

            return new RiskScoreDto
            {
                TransactionID = transaction.TransactionID,
                ScoreValue = riskScore.ScoreValue,
                ReasonsJSON = riskScore.ReasonsJSON
            };
        }

        public async Task<IEnumerable<RiskScore>> GetAllAsync() => await _context.RiskScores.ToListAsync();

        public async Task<RiskScore> GetByIdAsync(string id) =>
            await _context.RiskScores.FindAsync(id) ?? throw new NotFoundException("RiskScore not found");

        public async Task<RiskScore> UpdateAsync(RiskScore updated)
        {
            var existing = await _context.RiskScores.FindAsync(updated.ScoreId) ?? throw new NotFoundException("RiskScore not found");
            existing.ScoreValue = Math.Clamp(updated.ScoreValue, 0, 100);
            existing.ReasonsJSON = updated.ReasonsJSON;
            existing.EvaluatedAt = DateTime.UtcNow;

            _context.RiskScores.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(string id)
        {
            var score = await _context.RiskScores.FindAsync(id) ?? throw new NotFoundException("RiskScore not found");
            _context.RiskScores.Remove(score);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RiskScore>> SearchAsync(int transactionId) =>
            await _context.RiskScores.Where(r => r.TransactionID == transactionId).ToListAsync();
    }
}
