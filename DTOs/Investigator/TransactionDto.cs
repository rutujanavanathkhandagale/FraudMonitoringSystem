namespace FraudMonitoringSystem.DTOs.Investigator
{
    public class TransactionDto
    {
        public int TransactionID { get; set; }
        public string AccountId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerType { get; set; } = string.Empty;
        public string CounterpartyAccount { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string GeoLocation { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? SourceType { get; set; }
    }
}