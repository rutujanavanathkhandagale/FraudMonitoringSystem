using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs;
using FraudMonitoringSystem.DTOs.Rules;
using FraudMonitoringSystem.Models.Rules;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Rules;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations.Rules
{
    public class DetectionRuleRepository : IDetectionRuleRepository
    {
        private readonly WebContext _context;

        public DetectionRuleRepository(WebContext context) => _context = context;

        public async Task<List<DetectionRuleDto>> GetAllRulesAsync()
        {
            return await _context.DetectionRules
                .Include(r => r.Scenario)
                .Select(r => MapToDto(r))
                .ToListAsync();
        }

        public async Task<DetectionRuleDto?> GetRuleByIdAsync(int id)
        {
            var rule = await _context.DetectionRules.Include(r => r.Scenario)
                                                   .FirstOrDefaultAsync(r => r.RuleId == id);
            return rule == null ? null : MapToDto(rule);
        }

        public async Task<bool> AddRuleAsync(DetectionRule rule)
        {
            await _context.DetectionRules.AddAsync(rule);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<DetectionRuleDto?> UpdateRuleAsync(DetectionRuleDto dto)
        {
            var existing = await _context.DetectionRules.FindAsync(dto.RuleId);
            if (existing == null) return null;


            existing.Expression = dto.Expression;
            existing.Threshold = dto.Threshold;
            existing.Version = dto.Version;
            existing.Status = dto.Status;
            existing.ScenarioId = dto.ScenarioId;

            await _context.SaveChangesAsync();
            return await GetRuleByIdAsync(dto.RuleId);
        }

        public async Task<bool> DeleteRuleAsync(int id)
        {
            var rule = await _context.DetectionRules.FindAsync(id);
            if (rule != null)
            {
                _context.DetectionRules.Remove(rule);
                return await _context.SaveChangesAsync() > 0;
            } 
            return false;
        }

        public async Task<List<DetectionRuleDto>> GetRulesByScenarioAsync(int scenarioId)
        {
            return await _context.DetectionRules.Include(r => r.Scenario)
                .Where(r => r.ScenarioId == scenarioId && r.Status == "Active")
                .Select(r => MapToDto(r))
                .ToListAsync();
        }

        public async Task<List<DetectionRuleDto>> GetAllRulesByScenarioAsync(int scenarioId)
        {
            return await _context.DetectionRules.Include(r => r.Scenario)
                .Where(r => r.ScenarioId == scenarioId)
                .Select(r => MapToDto(r))
                .ToListAsync();
        }

        public async Task<List<DetectionRuleDto>> GetRulesByStatusAsync(string status)
        {
            return await _context.DetectionRules
                .Include(r => r.Scenario)
                .Where(r => r.Status == status)
                .Select(r => MapToDto(r))
                .ToListAsync();
        }


        private static DetectionRuleDto MapToDto(DetectionRule r) =>
     new DetectionRuleDto
     {
         RuleId = r.RuleId,
         Expression = r.Expression,
         Threshold = r.Threshold,
         Version = r.Version,
         CustomerType = r.CustomerType,
         Status = r.Status,
         ScenarioId = r.ScenarioId,
         Scenario = new ScenarioDto
         {
             ScenarioId = r.Scenario.ScenarioId,
             Name = r.Scenario.Name,
             Description = r.Scenario.Description,
             RiskDomain = r.Scenario.RiskDomain,
             Status = r.Scenario.Status
         }
     };

        
    }
}

