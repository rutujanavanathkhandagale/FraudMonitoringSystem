using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Models.Notification
{
    public class ChatMessage
    {
    [Key]
         
        public int MessageId { get; set; }
        public int CustomerId { get; set; }
        public string SenderRole { get; set; } // Customer / Analyst / Compliance
        public string MessageText { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
        public bool IsSeen { get; set; } = false;
        public ICollection<DocumentAttachment> Attachments { get; set; } = new List<DocumentAttachment>();
    }
}