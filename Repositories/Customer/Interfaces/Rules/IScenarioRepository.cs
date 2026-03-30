using FraudMonitoringSystem.Models.Rules;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.Rules
{
    public interface IScenarioRepository
    {
        Task<Scenario?> GetByIdAsync(int id);
        Task<IEnumerable<Scenario>> GetAllAsync();
        Task<int> AddAsync(Scenario scenario);
        Task UpdateAsync(Scenario scenario);
        Task DeleteAsync(int id);
        
    }
}
