using FraudMonitoringSystem.Models.AlertCase;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase
{
    public interface ICaseService
    {
        Task<Case> CreateCaseAsync(long CustomerId, string caseType, string priority);

        Task<List<Case>> GetAllCasesAsync();

        Task<string> UpdateCaseStatusAsync(int caseId, string status);
        Task<Case> UpdateCaseAsync(int caseId, string caseType, string priority);

    }
}
