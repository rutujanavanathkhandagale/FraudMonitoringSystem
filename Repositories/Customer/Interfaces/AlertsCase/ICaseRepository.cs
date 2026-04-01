using FraudMonitoringSystem.Models.AlertCase;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase
{
	public interface ICaseRepository
	{
		Task<IEnumerable<Case>> GetAllCases();

		Task<Case> GetCaseById(int id);

		Task AddCase(Case caseEntity);

		Task UpdateCase(Case caseEntity);

		Task DeleteCase(int id);
	}
}
