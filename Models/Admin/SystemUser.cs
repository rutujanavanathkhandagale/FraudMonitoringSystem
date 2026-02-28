using FraudMonitoringSystem.Models.Customer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraudMonitoringSystem.Models.Admin
{
    public class SystemUser
    {
        [Key]
        public int Id { get; set; }
        // Foreign Key to Registration
        [Required]
        public int RegistrationId { get; set; }
        [ForeignKey(nameof(RegistrationId))]
        public Registration Registration { get; set; }
        // Store role separately for quick filtering (Analyst, Admin, etc.)
        [Required]
        public AdminRole Role { get; set; }
        
    }
}