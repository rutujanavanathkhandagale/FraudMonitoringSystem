using FraudMonitoringSystem.Models.AlertCase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraudMonitoringSystem.Models.ComplianceOfficer
{
    public class Regulatory_Report
    {

        [Key]
        public int ReportID { get; set; }

        [Required]
        [ForeignKey("Case")]
        public int CaseID { get; set; }
        public virtual Case? Case { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "ReportType must contain only letters and spaces.")]
        public string ReportType { get; set; }

        [Required]
        public string Period { get; set; }


        [DataType(DataType.Date)]
        [Required]
        public DateTime SubmittedDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}
