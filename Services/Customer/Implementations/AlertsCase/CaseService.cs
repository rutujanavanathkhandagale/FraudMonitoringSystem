using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase.FraudMonitoringSystem.Services.AlertCase.Interfaces;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;

namespace FraudMonitoringSystem.Services.Customer.Implementations.AlertsCase
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _caseRepository;

        public CaseService(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public async Task<Case> CreateCaseAsync(long customerId, string caseType, string priority)
        {
            var newCase = new Case
            {
                CustomerId = customerId,
                CaseType = caseType,
                Priority = priority,
                Status = "Open"
            };
            return await _caseRepository.CreateCaseAsync(newCase);
        }

        public async Task<List<Case>> GetAllCasesAsync()
        {
            return await _caseRepository.GetAllCasesAsync();
        }

        public async Task<string> UpdateCaseStatusAsync(int caseId, string status)
        {
            var caseEntity = await _caseRepository.GetCaseByIdAsync(caseId);
            if (caseEntity == null)
                return "Case not found";

            caseEntity.Status = status;

            await _caseRepository.UpdateCaseAsync(caseEntity);

            return "Case status updated successfully";
        }
        public async Task<Case> UpdateCaseAsync(int caseId, string caseType, string priority)
        {
            var caseEntity = await _caseRepository.GetCaseByIdAsync(caseId);

            if (caseEntity == null)
                throw new Exception("Case not found");

            caseEntity.CaseType = caseType;
            caseEntity.Priority = priority;

            await _caseRepository.UpdateCaseAsync(caseEntity);

            return caseEntity;
        }


    }
}
