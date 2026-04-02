using System.Text.Json;
using FraudMonitoringSystem.DTOs.ComplianceOfficer;
using FraudMonitoringSystem.Models.DTOs;
using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;

public class TransactionPatternService : ITransactionPatternService
{
    private readonly ITransactionPatternRepository _repo;
    public TransactionPatternService(ITransactionPatternRepository repo) => _repo = repo;

    public async Task<TransactionPatternAnalysis> AnalyzePatternAsync(int customerId)
    {
        var customer = await _repo.GetCustomerByIdAsync(customerId);
        if (customer == null) return null;

        var txs = await _repo.GetTransactionsByCustomerIdAsync(customerId);
        var alertCount = await _repo.GetMappedAlertCountAsync(customerId);
        var maxAlert = await _repo.GetHighestSeverityAlertAsync(customerId);

        var result = new TransactionPatternAnalysis
        {
            TotalMappedAlerts = alertCount,
            HighestSeverity = maxAlert?.Severity ?? "Low"
        };

        // --- RULE 1: High-Risk Sources (Crypto/Offshore) ---
        var highRisk = new[] { "CryptoExchange", "ForeignTransfer", "ShellCompany" };
        if (txs.Any(t => highRisk.Contains(t.Channel)))
            result.Reasons.Add("FAIL: Interaction with High-Risk Source (Crypto).");

        // --- RULE 2: Trusted Source Exemption (Salary/Gov) ---
        var trusted = new[] { "Government", "LIC", "Salary", "PensionFund" };
        if (txs.Any(t => t.Amount > 1000000 && !trusted.Contains(t.Channel)))
            result.Reasons.Add("FAIL: Large Credit (>10L) from unverified source.");

        // --- RULE 3: Structuring (Frequency vs Customer Type) ---
        var maxDaily = txs.GroupBy(t => t.Timestamp.Date).Select(g => g.Count()).DefaultIfEmpty(0).Max();
        if (maxDaily >= 5)
        {
            if (customer.CustomerType == "Individual")
                result.Reasons.Add($"FAIL: Structuring Detected ({maxDaily} tx in one day).");
            else
                result.Reasons.Add("PASS: High daily frequency is normal for Corporate profile.");
        }

        // --- RULE 4: Rapid Deflation (Money Mule Check) ---
        var last24h = DateTime.Now.AddDays(-1);
        var credits = txs.Where(t => t.Timestamp > last24h && t.TransactionType == "Credit").Sum(t => t.Amount);
        var debits = txs.Where(t => t.Timestamp > last24h && t.TransactionType == "Debit").Sum(t => t.Amount);
        if (credits > 50000 && (debits / (credits == 0 ? 1 : credits)) > 0.95m)
            result.Reasons.Add("FAIL: Rapid Deflation (95% funds exited within 24h).");

        // --- RULE 5: Geographic Anomaly (Travel Check) ---
        var recent = txs.OrderByDescending(t => t.Timestamp).Take(2).ToList();
        if (recent.Count == 2 && (recent[0].Timestamp - recent[1].Timestamp).TotalHours < 1
            && recent[0].GeoLocation != recent[1].GeoLocation)
            result.Reasons.Add($"FAIL: Geo-Anomaly ({recent[1].GeoLocation} to {recent[0].GeoLocation} in <1hr).");

        // --- FINAL VERDICT & STORAGE ---
        int failCount = result.Reasons.Count(r => r.StartsWith("FAIL"));
        result.RiskScore = Math.Min(100, 10 + (failCount * 25));

        bool isSuspicious = result.RiskScore > 60 || result.HighestSeverity == "High";
        result.OverallResult = isSuspicious ? "FAIL" : "PASS";
        result.Status = isSuspicious ? "Suspicious" : "Not Suspicious";

        // Persist to RiskScore table
        await _repo.SaveRiskScoreAsync(new RiskScore
        {
            TransactionID = txs.FirstOrDefault()?.TransactionID ?? 0,
            ScoreValue = (int)result.RiskScore,
            ReasonsJSON = JsonSerializer.Serialize(result.Reasons),
            EvaluatedAt = DateTime.Now
        });

        return result;
    }
}