using FraudMonitoringSystem.Models.AlertCase;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase
{
    namespace FraudMonitoringSystem.Services.AlertCase.Interfaces
    {
        public interface ICaseRepository
        {
            Task<Case> CreateCaseAsync(Case caseEntity);

            Task<List<Case>> GetAllCasesAsync();

            Task<Case?> GetCaseByIdAsync(int id);

            Task UpdateCaseAsync(Case caseEntity);
        }
    }
}
