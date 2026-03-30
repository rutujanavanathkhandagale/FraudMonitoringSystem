using FraudMonitoringSystem.DTOs.Investigator;
using FraudMonitoringSystem.Models.Investigator;
using System.Collections.Generic;





namespace FraudMonitoringSystem.Services.Customer.Interfaces.Investigator
{
    public interface IRiskScoreService
    {
        // New explicit method for generating a score
        Task<RiskScoreDto> GenerateRiskScoreAsync(int transactionId);

        Task<IEnumerable<RiskScore>> GetAllAsync();
        Task<RiskScore> GetByIdAsync(string id);
        Task<RiskScore> UpdateAsync(RiskScore updated);
        Task DeleteAsync(string id);
        Task<IEnumerable<RiskScore>> SearchAsync(int transactionId);
    }
}
