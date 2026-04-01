using System.ComponentModel.DataAnnotations;
using FraudMonitoringSystem.Models.AlertCase;
using System.Text.Json.Serialization;
namespace FraudMonitoringSystem.Models.AlertsCase
{
	public class InvestigationNote
	{
		[Key]
		public int NoteID { get; set; }

		public int CaseID { get; set; }

		public int AuthorID { get; set; }

		public string? NoteText { get; set; }

		public DateTime CreatedDate { get; set; }

		// Navigation Property
		[JsonIgnore]
		public Case? Case { get; set; }
	}
}
