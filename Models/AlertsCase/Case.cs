
using FraudMonitoringSystem.Models.Customer;

using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace FraudMonitoringSystem.Models.AlertCase

{

    public class Case

    {

        [Key]

        public int CaseID { get; set; }

        [Required]
        public long CustomerId { get; set; }

        [ForeignKey("CustomerId")]

        public PersonalDetails PrimaryCustomer { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required, MaxLength(30)]

        public string CaseType { get; set; } = string.Empty; // Fraud / AML

        [Required, MaxLength(20)]

        public string Priority { get; set; } = string.Empty; // Low / Medium / High

        [Required, MaxLength(20)]

        public string Status { get; set; } = "Open"; // Open / Investigating / Resolved

        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    }

}
