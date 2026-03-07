using System;
using System.ComponentModel.DataAnnotations;
namespace FraudMonitoringSystem.Models.ComplianceOfficer
{
    public class PEPListModel
    {
        [Key]
        public long PepId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        public DateOnly DOB { get; set; }
        [Required]
        [MaxLength(250)]
        public string PermanentAddress { get; set; }
    }
}