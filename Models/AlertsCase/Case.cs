
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.Models.Customer;
using System.Text.Json.Serialization;
namespace FraudMonitoringSystem.Models.AlertCase

{

    public class Case

    {

        [Key]

        public int CaseID { get; set; }

        [Required]
        public long CustomerId { get; set; }

        [ForeignKey("CustomerId")]

        //public int PrimaryCustomerID { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

		[Required]
		[RegularExpression("Fraud|AML", ErrorMessage = "CaseType must be Fraud or AML")]
		

		public string CaseType { get; set; }  // Fraud / AML

        [Required, MaxLength(20)]

        public string Priority { get; set; } = string.Empty; // Low / Medium / High

        [Required, MaxLength(20)]

        public string Status { get; set; } = "Open"; // Open / Investigating / Resolved

        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
        [JsonIgnore]
		public ICollection<AlertCaseMapping> AlertCaseMappings { get; set; }
		[JsonIgnore]
		public ICollection<InvestigationNote> InvestigationNotes { get; set; }
		[JsonIgnore]
		public ICollection<CaseAttachment> CaseAttachments { get; set; }
		public int TransactionId { get; internal set; }
		//public int AlertId { get; internal set; }
	}

}
