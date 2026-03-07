using FraudMonitoringSystem.Models;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase
{
    public interface IAlertService
    {
        Task<Alert> CreateAlertAsync(int transactionId, int ruleId, string severity);

        Task<List<Alert>> GetAllAlertsAsync();

        Task<string> AssignAlertToCaseAsync(int alertId, int caseId);
    }
}
