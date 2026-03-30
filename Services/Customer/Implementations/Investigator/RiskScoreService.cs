using FraudMonitoringSystem.DTOs.Investigator;
using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Investigator;
using FraudMonitoringSystem.Services.Customer.Interfaces.Investigator;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Investigator
{
    public class RiskScoreService : IRiskScoreService
    {
        private readonly IRiskScoreRepository _riskScoreRepository;

        public RiskScoreService(IRiskScoreRepository riskScoreRepository)
        {
            _riskScoreRepository = riskScoreRepository;
        }

        // Triggers the complex repository evaluation logic
        public async Task<RiskScoreDto> GenerateRiskScoreAsync(int transactionId)
        {
            return await _riskScoreRepository.EvaluateRiskScoreAsync(transactionId);
        }

        public async Task<IEnumerable<RiskScore>> GetAllAsync() =>
            await _riskScoreRepository.GetAllAsync();

        public async Task<RiskScore> GetByIdAsync(string id) =>
            await _riskScoreRepository.GetByIdAsync(id);

        public async Task<RiskScore> UpdateAsync(RiskScore updated) =>
            await _riskScoreRepository.UpdateAsync(updated);

        public async Task DeleteAsync(string id) =>
            await _riskScoreRepository.DeleteAsync(id);

        public async Task<IEnumerable<RiskScore>> SearchAsync(int transactionId) =>
            await _riskScoreRepository.SearchAsync(transactionId);
    }
}

