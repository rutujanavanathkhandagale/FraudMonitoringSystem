
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase.FraudMonitoringSystem.Services.AlertCase.Interfaces;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;

namespace FraudMonitoringSystem.Services.Customer.Implementations.AlertsCase
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        private readonly ICaseRepository _caseRepository;

        public AlertService(IAlertRepository alertRepository,
                            ICaseRepository caseRepository)
        {
            _alertRepository = alertRepository;
            _caseRepository = caseRepository;
        }

        public async Task<Alert> CreateAlertAsync(int transactionId, int ruleId, string severity)
        {
            var alert = new Alert
            {
                TransactionID = transactionId,
                RuleID = ruleId,
                Severity = severity,
                Status = "Open"
            };

            return await _alertRepository.CreateAlertAsync(alert);
        }

        public async Task<List<Alert>> GetAllAlertsAsync()
        {
            return await _alertRepository.GetAllAlertsAsync();
        }

        public async Task<string> AssignAlertToCaseAsync(int alertId, int caseId)
        {
            var alert = await _alertRepository.GetAlertByIdAsync(alertId);
            if (alert == null)
                return "Alert not found";

            var caseEntity = await _caseRepository.GetCaseByIdAsync(caseId);
            if (caseEntity == null)
                return "Case not found";

            alert.CaseID = caseId;
            alert.Status = "InReview";

            await _alertRepository.UpdateAlertAsync(alert);

            return "Alert assigned to case successfully";
        }
    }
}
