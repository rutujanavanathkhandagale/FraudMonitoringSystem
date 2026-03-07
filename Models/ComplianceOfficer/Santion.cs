using System;

using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Models.ComplianceOfficer

{

    public class Sanction

    {

        [Key]

        public long SanctionId { get; set; }

        [Required]

        public string FirstName { get; set; }

        [Required]

        public DateOnly DOB { get; set; }

        [Required]

        public string PermanentAddress { get; set; }

    }

}
