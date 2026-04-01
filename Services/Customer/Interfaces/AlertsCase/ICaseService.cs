using FraudMonitoringSystem.Models.AlertCase;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase
{
    public interface ICaseService
    {
		Task<IEnumerable<Case>> GetAllCases();
		Task<Case> GetCaseById(int id);
		Task<Case> CreateCase(Case caseObj);
		Task<Case> UpdateCaseStatus(int id, string status);
		Task DeleteCase(int id);
		Task<IEnumerable<Case>> GetAmlCases();
		Task<IEnumerable<Case>> GetFraudCases();

	}
}
