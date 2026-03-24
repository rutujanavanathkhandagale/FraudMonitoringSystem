using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Models.Notification
{
    public class Notification
    {
    [Key]
        public int NotificationId { get; set; }
        public int CustomerId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }

}
