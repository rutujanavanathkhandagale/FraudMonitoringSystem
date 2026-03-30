using FraudMonitoringSystem.DTOs.Investigator;
using FraudMonitoringSystem.Models.Investigator;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.Investigator
{
    public interface IRiskScoreRepository
    {
        Task<RiskScoreDto> EvaluateRiskScoreAsync(int transactionId);
        Task<IEnumerable<RiskScore>> GetAllAsync();
        Task<RiskScore> GetByIdAsync(string id);
        Task<RiskScore> UpdateAsync(RiskScore updated);
        Task DeleteAsync(string id);
        Task<IEnumerable<RiskScore>> SearchAsync(int transactionId);
    }
}