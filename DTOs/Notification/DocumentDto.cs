namespace FraudMonitoringSystem.DTOs.Notification
{
    public class DocumentDto
    {
        public int CustomerId { get; set; }
        public int ChatMessageId { get; set; }
        public IFormFile File { get; set; }
    }
}
