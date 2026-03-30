using FraudMonitoringSystem.Exceptions.Rules;
using FraudMonitoringSystem.Models.Rules;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Rules;
using FraudMonitoringSystem.Services.Customer.Interfaces.Rules;
using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Rules
{
    public class ScenarioService : IScenarioService
    {
        private readonly IScenarioRepository _repository;
        private readonly IDetectionRuleRepository _ruleRepository;

        public ScenarioService(IScenarioRepository repository, IDetectionRuleRepository ruleRepository)
        {
            _repository = repository;
            _ruleRepository = ruleRepository;
        }

        public async Task<Scenario?> GetScenarioByIdAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Invalid Scenario ID.");

            var scenario = await _repository.GetByIdAsync(id);
            if (scenario == null)
                throw new NotFoundException($"Scenario with ID {id} not found.");

            return scenario;
        }

        public async Task<IEnumerable<Scenario>> GetAllScenariosAsync() =>
            await _repository.GetAllAsync();

        public async Task<int> CreateScenarioAsync(Scenario scenario)
        {
            if (string.IsNullOrWhiteSpace(scenario.Name))
                throw new BadRequestException("Scenario Name cannot be empty.");

            var id = await _repository.AddAsync(scenario);
            if (id <= 0)
                throw new BadRequestException("Failed to create Scenario.");

            return id;
        }

        public async Task<bool> UpdateScenarioAsync(Scenario scenario)
        {
            if (scenario.ScenarioId <= 0)
                throw new BadRequestException("Invalid Scenario ID.");

            var existing = await _repository.GetByIdAsync(scenario.ScenarioId);
            if (existing == null)
                throw new NotFoundException($"Scenario with ID {scenario.ScenarioId} not found.");

            existing.Name = scenario.Name;
            existing.Description = scenario.Description;
            existing.RiskDomain = scenario.RiskDomain;
            existing.Status = scenario.Status;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteScenarioAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Invalid Scenario ID.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException($"Scenario with ID {id} not found.");

            await _repository.DeleteAsync(id);
            return true;
        }
        
    }
}