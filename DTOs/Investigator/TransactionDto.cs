namespace FraudMonitoringSystem.DTOs.Investigator
{
    public class TransactionDto
    {
        public int TransactionID { get; set; }   // ✅ Added to match model
        public int CustId { get; set; }
        public string CounterAccount { get; set; }
        public decimal Amount { get; set; }
        public string Location { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string Channel { get; set; }
        public string TransactionType { get; set; }
        public string SourceType { get; set; }
    }
}
