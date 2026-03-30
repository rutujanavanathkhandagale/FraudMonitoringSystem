using FraudMonitoringSystem.Models.Rules;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.Rules
{
    public interface IScenarioService
    {
        Task<Scenario?> GetScenarioByIdAsync(int id);
        Task<IEnumerable<Scenario>> GetAllScenariosAsync();
        Task<int> CreateScenarioAsync(Scenario scenario);
        Task<bool> UpdateScenarioAsync(Scenario scenario);
        Task<bool> DeleteScenarioAsync(int id);

    }
}