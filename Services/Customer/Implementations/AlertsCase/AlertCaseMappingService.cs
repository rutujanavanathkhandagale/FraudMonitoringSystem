using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
namespace FraudMonitoringSystem.Services.Customer.Implementations.AlertsCase
{
	public class AlertCaseMappingService : IAlertCaseMappingService
	{
		private readonly IAlertCaseMappingRepository _repository;

		public AlertCaseMappingService(IAlertCaseMappingRepository repository)
		{
			_repository = repository;
		}

		public async Task AddMapping(AlertCaseMapping mapping)
		{
			await _repository.AddMapping(mapping);
		}

		public async Task<IEnumerable<AlertCaseMapping>> GetMappingsByCase(int caseId)
		{
			return await _repository.GetMappingsByCase(caseId);
		}
	}
}

	

