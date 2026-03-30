using FraudMonitoringSystem.DTOs;
using FraudMonitoringSystem.DTOs.Rules;
using FraudMonitoringSystem.Exceptions.Rules;
using FraudMonitoringSystem.Models.Rules;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Rules;
using FraudMonitoringSystem.Services.Customer.Interfaces.Rules;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Rules
{
    public class DetectionRuleService : IDetectionRuleService
    {
        private readonly IDetectionRuleRepository _repository;

        public DetectionRuleService(IDetectionRuleRepository repository) => _repository = repository;

        public async Task<List<DetectionRuleDto>> GetAllRulesAsync() =>
            await _repository.GetAllRulesAsync();

        public async Task<DetectionRuleDto?> GetRuleByIdAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Invalid Rule ID.");

            var rule = await _repository.GetRuleByIdAsync(id);
            if (rule == null)
                throw new NotFoundException($"DetectionRule with ID {id} not found.");

            return rule;
        }

        public async Task<string> AddRuleAsync(DetectionRuleCreateDto dto)
        {
            if (dto.ScenarioId <= 0)
                throw new BadRequestException("Invalid Scenario ID.");
            if (string.IsNullOrWhiteSpace(dto.Expression))
                throw new BadRequestException("Expression cannot be empty.");
            if (string.IsNullOrWhiteSpace(dto.CustomerType))
                throw new BadRequestException("CustomerType cannot be empty.");
            if (dto.Threshold < 0)
                throw new BadRequestException("Threshold must be non-negative.");

            var entity = new DetectionRule
            {
                Expression = dto.Expression,
                Threshold = dto.Threshold,
                Version = dto.Version,
                CustomerType = dto.CustomerType,
                Status = dto.Status,
                ScenarioId = dto.ScenarioId
            };

            var result = await _repository.AddRuleAsync(entity);
            if (!result)
                throw new BadRequestException("Failed to add DetectionRule.");

            return "DetectionRule added successfully";
        }

        public async Task<DetectionRuleDto?> UpdateRuleAsync(DetectionRuleDto dto)
        {
            if (dto.RuleId <= 0)
                throw new BadRequestException("Invalid Rule ID.");

            var existing = await _repository.GetRuleByIdAsync(dto.RuleId);
            if (existing == null)
                throw new NotFoundException($"DetectionRule with ID {dto.RuleId} not found.");

            var updated = await _repository.UpdateRuleAsync(dto);
            if (updated == null)
                throw new BadRequestException("Failed to update DetectionRule.");

            return updated;
        }

        public async Task<string> DeleteRuleAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Invalid Rule ID.");

            var existing = await _repository.GetRuleByIdAsync(id);
            if (existing == null)
                throw new NotFoundException($"DetectionRule with ID {id} not found.");

            var result = await _repository.DeleteRuleAsync(id);
            if (!result)
                throw new BadRequestException("Failed to delete DetectionRule.");

            return "DetectionRule deleted successfully";
        }

        public async Task<List<DetectionRuleDto>> GetRulesByScenarioAsync(int scenarioId)
        {
            if (scenarioId <= 0)
                throw new BadRequestException("Invalid Scenario ID.");

            var rules = await _repository.GetRulesByScenarioAsync(scenarioId);
            if (rules == null || rules.Count == 0)
                throw new NotFoundException($"No active rules found for Scenario ID {scenarioId}.");

            return rules;
        }

        public async Task<List<DetectionRuleDto>> GetAllRulesByScenarioAsync(int scenarioId)
        {
            if (scenarioId <= 0)
                throw new BadRequestException("Invalid Scenario ID.");

            var rules = await _repository.GetAllRulesByScenarioAsync(scenarioId);
            if (rules == null || rules.Count == 0)
                throw new NotFoundException($"No rules found for Scenario ID {scenarioId}.");

            return rules;
        }

        public async Task<List<DetectionRuleDto>> GetRulesByStatusAsync(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new BadRequestException("Status is required.");

            var rules = await _repository.GetRulesByStatusAsync(status);
            if (rules == null || rules.Count == 0)
                throw new NotFoundException($"No rules found with status {status}.");

            return rules;
        }
    }
}