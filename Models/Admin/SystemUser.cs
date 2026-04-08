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


        [Required]

        public string RoleId { get; set; }


        [ForeignKey(nameof(RoleId))]

        public Role Role { get; set; }


        // chnaged here
        [Required]

        public bool IsApproved { get; set; } = false;


        public DateTime? ApprovedAt { get; set; }


        // Admin who approved (RegistrationId of Admin)
        public int? ApprovedBy { get; set; }



    }


}