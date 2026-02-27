using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraudMonitoringSystem.Models.Admin
{
    public class RolePermission : BaseEntity
    {
        [Key]
        public int RolePermissionId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int PermissionId { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public int? AssignedBy { get; set; }
        // Navigation
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }
    }
}