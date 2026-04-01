using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Models.Rules;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.Models;
using System.Text.Json.Serialization;
namespace FraudMonitoringSystem.Models
{

	public class Alert
	{
		public int AlertID { get; set; }

		public int TransactionID { get; set; }

		public int RuleID { get; set; }

		public required string Severity { get; set; }

		public DateTime CreatedDate { get; set; }

		public string Status { get; set; }

		// Navigation Property
		[JsonIgnore]
		public ICollection<AlertCaseMapping> AlertCaseMappings { get; set; }
	}
}