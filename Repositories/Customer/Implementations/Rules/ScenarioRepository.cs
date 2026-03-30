using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.Rules;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Rules;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations.Rules
{
    public class ScenarioRepository : IScenarioRepository
    {
        private readonly WebContext _context;

        public ScenarioRepository(WebContext context) => _context = context;

        public async Task<Scenario?> GetByIdAsync(int id) =>
            await _context.Scenarios.FindAsync(id);

        public async Task<IEnumerable<Scenario>> GetAllAsync() =>
            await _context.Scenarios.ToListAsync();

        public async Task<int> AddAsync(Scenario scenario)
        {
            await _context.Scenarios.AddAsync(scenario);
            await _context.SaveChangesAsync();
            return scenario.ScenarioId;
        }

        public async Task UpdateAsync(Scenario scenario)
        {
            _context.Scenarios.Update(scenario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var scenario = await _context.Scenarios.FindAsync(id);
            if (scenario != null)
            {
                _context.Scenarios.Remove(scenario);
                await _context.SaveChangesAsync();
            }
        }
    }
}