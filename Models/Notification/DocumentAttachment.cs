using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Models.Notification
{
    public class DocumentAttachment
    {
    [Key]
        public int DocumentId { get; set; }
        public int ChatMessageId { get; set; }
        public ChatMessage ChatMessage { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }

}
