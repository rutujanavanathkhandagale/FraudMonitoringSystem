using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Models.Investigator;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase
{
    public interface IAlertService
    {
		//  Get all alerts
		Task<IEnumerable<Alert>> GetAllAlerts();

		//  Get by ID (MUST return Alert)
		Task<Alert> GetAlertById(int id);

		// Create (return created object)
		Task<Alert> CreateAlert(Alert alert);

		//  Update (return updated object)
		Task<Alert> UpdateAlert(int id, string status);

		//  Delete (no return needed)
		Task DeleteAlert(int id);
		Task GenerateAlertsFromRiskScores(List<RiskScore> scores);
		Task<Case> UpdateCaseStatus(int caseId, string status);


	}
}
