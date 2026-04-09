using System.Text.Json;
using FraudMonitoringSystem.DTOs.ComplianceOfficer;
using FraudMonitoringSystem.Models.DTOs;
using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;

public class TransactionPatternService : ITransactionPatternService
{
    private readonly ITransactionPatternRepository _repo;

    public TransactionPatternService(ITransactionPatternRepository repo)
    {
        _repo = repo;
    }

    public async Task<TransactionPatternAnalysis> AnalyzeAsync(int customerId, int transactionId)
    {
        // Get Current Transaction & Customer History
        var tx = await _repo.GetTransactionByIdAsync(transactionId);
        if (tx == null || tx.CustomerId != customerId) return null;

        var txs = await _repo.GetTransactionsByCustomerIdAsync(customerId);
        if (txs == null || !txs.Any()) return null;

        //  Pattern Metadata
        var totalTx = txs.Count;
        var avgAmount = txs.Average(t => t.Amount);
        var maxAmount = txs.Max(t => t.Amount);
        var minAmount = txs.Min(t => t.Amount);
        var last7Days = txs.Count(t => t.Timestamp >= DateTime.Now.AddDays(-7));

        // Risk & Source Configuration
        var risk = await _repo.GetRiskScoreByTransactionIdAsync(transactionId);
        decimal riskScore = risk?.ScoreValue ?? 30;

        var trustedSources = new List<string> { "LIC", "PF", "Pension", "Government", "Insurance", "Gratuity", "Salary", "ProvidentFund", "Scholarship", "LoanDisbursement" };
        var highRiskSources = new List<string> { "CryptoExchange", "Foreign Exchange", "ShellCompany", "OffshoreAccount" };

        var reasons = new List<string>();

        // RULE: HIGH RISK SOURCE (From Repository Logic)
        if (highRiskSources.Contains(tx.SourceType))
        {
            reasons.Add("FAIL: Transaction involves High-Risk Source (Crypto/Offshore)");
        }

        //RULE: LARGE CREDIT FROM NON-TRUSTED SOURCE (From Repository Logic)
        if (tx.TransactionType == "Credit" && tx.Amount > 1000000 && !trustedSources.Contains(tx.SourceType))
        {
            reasons.Add("FAIL: Large credit (>1M) from non-trusted source");
        }

       
        // 5 or more small transactions (< 50000) on the same day
        var structuring = txs
            .Where(t => t.Amount < 50000)
            .GroupBy(t => t.Timestamp.Date)
            .Any(g => g.Count() >= 5);

        if (structuring)
        {
            reasons.Add("FAIL: Potential Structuring (5+ small transactions in one day)");
        }

        //TRUSTED SOURCE OVERRIDE (Existing Logic)
        bool isTrustedSource = trustedSources.Contains(tx.SourceType);
        bool isCredit = tx.TransactionType == "Credit";

        if (isTrustedSource && isCredit && reasons.Count == 0) // Only override if no high-risk flags exist
        {
            reasons.Add("PASS: Large credit from trusted source");
            return CreateAnalysisObject(tx, txs, "PASS", "Not Suspicious", riskScore, "Low", reasons);
        }

        //STANDARD BEHAVIORAL RULES
        if (tx.Amount > avgAmount * 3)
            reasons.Add("FAIL: Transaction 3x higher than customer average");

        if (tx.Amount > maxAmount)
            reasons.Add("FAIL: Transaction exceeds historical maximum");

        var rapid = txs.Count(t => t.Timestamp >= tx.Timestamp.AddHours(-1));
        if (rapid >= 3)
            reasons.Add("FAIL: Rapid transactions (3+ per hour)");

        // FINAL CALCULATIONS
        // A Fail is triggered if there are 2+ reasons OR if a high-risk source is involved OR risk score > 60
        bool isTransactionFail = reasons.Count >= 2 || highRiskSources.Contains(tx.SourceType) || riskScore > 60;

        string transactionResult = isTransactionFail ? "FAIL" : "PASS";
        string transactionStatus = isTransactionFail ? "Suspicious" : "Not Suspicious";
        string severity = riskScore > 80 ? "High" : (riskScore > 60 ? "Medium" : "Low");

        // External Reasons from JSON
        if (!string.IsNullOrEmpty(risk?.ReasonsJSON))
        {
            try
            {
                var extReasons = JsonSerializer.Deserialize<List<string>>(risk.ReasonsJSON);
                if (extReasons != null) reasons.AddRange(extReasons);
            }
            catch { }
        }

        return CreateAnalysisObject(tx, txs, transactionResult, transactionStatus, riskScore, severity, reasons);
    }

    // Helper method to keep code clean
    private TransactionPatternAnalysis CreateAnalysisObject(Transaction tx, List<Transaction> txs, string result, string status, decimal riskScore, string severity, List<string> reasons)
    {
        return new TransactionPatternAnalysis
        {
            TransactionResult = result,
            TransactionStatus = status,
            CustomerResult = txs.Count >= 5 ? "PASS" : "FAIL",
            CustomerStatus = txs.Count >= 5 ? "Normal Behavior" : "Needs Review",
            RiskScore = riskScore,
            HighestSeverity = severity,
            TotalMappedAlerts = reasons.Count,
            Reasons = reasons,
            CurrentTransactionAmount = tx.Amount,
            CurrentTransactionType = tx.TransactionType,
            CurrentTransactionChannel = tx.Channel,
            CurrentTransactionDate = tx.Timestamp,
            TotalTransactions = txs.Count,
            AverageAmount = txs.Average(t => t.Amount),
            MaxAmount = txs.Max(t => t.Amount),
            MinAmount = txs.Min(t => t.Amount),
            Last7DaysCount = txs.Count(t => t.Timestamp >= DateTime.Now.AddDays(-7))
        };
    }
}