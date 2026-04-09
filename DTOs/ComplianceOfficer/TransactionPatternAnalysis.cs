namespace FraudMonitoringSystem.Models.DTOs
{
    public class TransactionPatternAnalysis

    {

        //TRANSACTION RESULT

        public string TransactionResult { get; set; }

        public string TransactionStatus { get; set; }

        // CUSTOMER RESULT

        public string CustomerResult { get; set; }

        public string CustomerStatus { get; set; }

        public decimal RiskScore { get; set; }

        public string HighestSeverity { get; set; }

        public int TotalMappedAlerts { get; set; }

        public List<string> Reasons { get; set; } = new List<string>();

        // Transaction Details

        public decimal CurrentTransactionAmount { get; set; }

        public string CurrentTransactionType { get; set; }

        public string CurrentTransactionChannel { get; set; }

        public DateTime CurrentTransactionDate { get; set; }

        // Customer Pattern

        public int TotalTransactions { get; set; }

        public decimal AverageAmount { get; set; }

        public decimal MaxAmount { get; set; }

        public decimal MinAmount { get; set; }

        public int Last7DaysCount { get; set; }

    }

}