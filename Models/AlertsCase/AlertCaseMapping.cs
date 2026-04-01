using FraudMonitoringSystem.Models.AlertCase;

namespace FraudMonitoringSystem.Models.AlertsCase
{
	public class AlertCaseMapping
	{
		public int Id { get; set; } 

		public int AlertID { get; set; }
		public Alert Alert { get; set; }   

		public int CaseID { get; set; }
		public Case Case { get; set; }
	}

}
