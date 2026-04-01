using System.ComponentModel.DataAnnotations;
using FraudMonitoringSystem.Models.AlertCase;

namespace FraudMonitoringSystem.Models.AlertsCase
{
	public class CaseAttachment
	{
		[Key]
		public int AttachmentID { get; set; }

		public int CaseID { get; set; }

		public string? FileURI { get; set; }

		public int UploadedBy { get; set; }

		public DateTime UploadedDate { get; set; }

		// Navigation Property
		public Case? Case { get; set; }
	}
}
