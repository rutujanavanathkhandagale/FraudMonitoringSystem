
namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase
{
    public interface IAlertRepository
    {
        Task<Alert> CreateAlertAsync(Alert alert);

        Task<List<Alert>> GetAllAlertsAsync();

        Task<Alert?> GetAlertByIdAsync(int id);

        Task UpdateAlertAsync(Alert alert);
    }
}
