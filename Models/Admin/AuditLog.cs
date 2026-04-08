using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Models.Admin
{
    public class AuditLog
    {
        [Key]
        public int AuditLogId { get; set; }

        [Required]
        [MaxLength(50)]
        public string EntityType { get; set; } = string.Empty;
        // "SystemUser", "Role"
        [Required]
        public int EntityId { get; set; }
        // SystemUser.Id OR RoleId mapped as int (if needed later)
        [Required]
        [MaxLength(50)]
        public string Action { get; set; } = string.Empty;
        // CREATE, APPROVE, DELETE, UPDATE
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int PerformedBy { get; set; }
        // Admin RegistrationId

        [Required]
        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
    }
}
 
 

