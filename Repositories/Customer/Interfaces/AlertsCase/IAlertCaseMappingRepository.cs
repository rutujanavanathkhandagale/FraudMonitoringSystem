using FraudMonitoringSystem.Models.AlertsCase;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase
{
	public interface IAlertCaseMappingRepository
	{
		Task AddMapping(AlertCaseMapping mapping);

		Task<IEnumerable<AlertCaseMapping>> GetMappingsByCase(int caseId);
	}
}
