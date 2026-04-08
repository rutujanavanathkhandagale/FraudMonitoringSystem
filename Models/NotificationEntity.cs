namespace FraudMonitoringSystem.Models
{
    public class NotificationEntity
    {
        public int Id { get; set; }
        public long CustomerId { get; set; }   // long type for large numeric IDs
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
