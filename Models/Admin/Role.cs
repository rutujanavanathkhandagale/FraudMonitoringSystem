using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Models.Admin
{
    public class Role : BaseEntity

    {

        [Key]

        public int RoleId { get; set; }

        [Required]

        [MaxLength(100)]

        public string RoleName { get; set; }

        [MaxLength(255)]

        public string Description { get; set; }

        // Navigation


        public ICollection<RolePermission> RolePermissions { get; set; }

    }

}