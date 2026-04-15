namespace FraudMonitoringSystem.DTOs.AlertCase
{
	public class AlertDTO
	{
		public int AlertID { get; set; }
		public int TransactionID { get; set; }

		//public int RuleID { get; set; }

		public string? Severity { get; set; }

		public string? Status { get; set; }
		//public string ReasonJSON { get; set; }
		public List<AlertReasonDto> ReasonDetails { get; set; }
		public DateTime CreatedDate { get; internal set; }
	}
}
