using FraudMonitoringSystem.DTOs;
using FraudMonitoringSystem.DTOs.Rules;
using FraudMonitoringSystem.Models.Rules;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.Rules
{
    public interface IDetectionRuleRepository
    {
        Task<List<DetectionRuleDto>> GetAllRulesAsync();
        Task<DetectionRuleDto?> GetRuleByIdAsync(int id);
        Task<bool> AddRuleAsync(DetectionRule rule);
        Task<DetectionRuleDto?> UpdateRuleAsync(DetectionRuleDto dto);
        Task<bool> DeleteRuleAsync(int id);
        Task<List<DetectionRuleDto>> GetRulesByScenarioAsync(int scenarioId);
        Task<List<DetectionRuleDto>> GetAllRulesByScenarioAsync(int scenarioId);
        Task<List<DetectionRuleDto>> GetRulesByStatusAsync(string status);

        
    }
}