using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Models.Admin
{
    public class AuditLog
    {
        [Key]
        public int AuditLogId { get; set; }

        [Required, MaxLength(50)]
        public string EntityType { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string EntityId { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Action { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int PerformedBy { get; set; }
        public string? PerformedByName { get; set; }

        [Required]
        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
    }
}