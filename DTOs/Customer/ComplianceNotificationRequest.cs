namespace FraudMonitoringSystem.DTOs.Customer
{
    public class ComplianceNotificationRequest
    {
        public long CustomerId { get; set; }
        public string Message { get; set; }
    }

}