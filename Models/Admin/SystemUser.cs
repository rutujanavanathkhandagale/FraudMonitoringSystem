using FraudMonitoringSystem.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraudMonitoringSystem.Models.Admin
{
    public class SystemUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string SystemUserCode { get; set; } = string.Empty;

        [Required]
        public int RegistrationId { get; set; }

        [ForeignKey(nameof(RegistrationId))]
        public Registration Registration { get; set; }

        // ✅ Current role
        public string? RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }

        // ✅ Store removed role
        public string? LastAssignedRoleId { get; set; }

        public string? LastAssignedRoleName { get; set; }

        // ✅ Approval & activity status
        [Required]
        public bool IsApproved { get; set; } = false;

        // ✅ IMPORTANT FIX (was true before ❌)
        public bool IsActive { get; set; } = false;

        public DateTime? ApprovedAt { get; set; }

        // Admin who approved
        public int? ApprovedBy { get; set; }
    }
}