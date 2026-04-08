using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Models.Admin
{
    public class Role : BaseEntity

    {

        [Key]

        public string RoleId { get; set; } = string.Empty; // R101 logic

        [Required]

        [MaxLength(100)]

        public string RoleName { get; set; } = string.Empty;


        [MaxLength(255)]

        public string? Description { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }


}