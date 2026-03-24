namespace FraudMonitoringSystem.DTOs.Notification
{
    public class ChatMessageDto
    {
        public int CustomerId { get; set; }
        public string SenderRole { get; set; }
        public string MessageText { get; set; }
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
