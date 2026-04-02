namespace FraudMonitoringSystem.Models.DTOs
{
    public class TransactionPatternAnalysis
    {
        public string OverallResult { get; set; } // "PASS" or "FAIL"
        public string Status { get; set; } // "Suspicious" or "Not Suspicious"
        public decimal RiskScore { get; set; }
        public string HighestSeverity { get; set; }
        public int TotalMappedAlerts { get; set; }
        public List<string> Reasons { get; set; } = new List<string>();
    }
}