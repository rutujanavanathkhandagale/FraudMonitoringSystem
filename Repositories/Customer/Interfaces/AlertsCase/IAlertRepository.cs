
using FraudMonitoringSystem.Models;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase
{
    public interface IAlertRepository
    {
		Task<IEnumerable<Alert>> GetAllAlerts();

		Task<Alert> GetAlertById(int id);

		Task AddAlert(Alert alert);

		Task UpdateAlert(Alert alert);

		Task DeleteAlert(int id);
		Task AddAlert(object alert);
	}
}
