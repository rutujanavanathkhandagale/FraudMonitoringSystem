using FraudMonitoringSystem.DTOs;
using FraudMonitoringSystem.DTOs.Rules;
using FraudMonitoringSystem.Models.Rules;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.Rules
{
    public interface IDetectionRuleService
    {
        Task<List<DetectionRuleDto>> GetAllRulesAsync();
        Task<DetectionRuleDto?> GetRuleByIdAsync(int id);
        Task<string> AddRuleAsync(DetectionRuleCreateDto dto);
        Task<DetectionRuleDto?> UpdateRuleAsync(DetectionRuleDto dto);
        Task<string> DeleteRuleAsync(int id);
        Task<List<DetectionRuleDto>> GetRulesByScenarioAsync(int scenarioId);
        Task<List<DetectionRuleDto>> GetAllRulesByScenarioAsync(int scenarioId);
        Task<List<DetectionRuleDto>> GetRulesByStatusAsync(string status);

       
    }
}