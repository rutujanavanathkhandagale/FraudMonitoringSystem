using FraudMonitoringSystem.Models.AlertsCase;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase
{
	public interface IAlertCaseMappingService
	{
		Task AddMapping(AlertCaseMapping mapping);
		Task<IEnumerable<AlertCaseMapping>> GetMappingsByCase(int caseId);
	}
}
