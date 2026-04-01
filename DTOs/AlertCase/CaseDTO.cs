namespace FraudMonitoringSystem.DTOs.AlertCase
{
	public class CaseDTO
	{
		//public int PrimaryCustomerID { get; set; }

		public string? CaseType { get; set; }

		public string? Priority { get; set; }

		public string? Status { get; set; }
		public long CustomerId { get; set; }
		public int TransactionId{ get; set; }
	}
}
