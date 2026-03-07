using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase.FraudMonitoringSystem.Services.AlertCase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations.AlertsCase
{
    public class CaseRepository : ICaseRepository
    {
        private readonly WebContext _context;

        public CaseRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<Case> CreateCaseAsync(Case caseEntity)
        {
            await _context.Cases.AddAsync(caseEntity);
            await _context.SaveChangesAsync();
            return caseEntity;
        }

        public async Task<List<Case>> GetAllCasesAsync()
        {
            return await _context.Cases
                .Include(c => c.PrimaryCustomer)
                .Include(c => c.Alerts)
                .ToListAsync();
        }

        public async Task<Case?> GetCaseByIdAsync(int id)
        {
            return await _context.Cases
                .Include(c => c.PrimaryCustomer)
                .Include(c => c.Alerts)
                .FirstOrDefaultAsync(c => c.CaseID == id);
        }

        public async Task UpdateCaseAsync(Case caseEntity)
        {
            _context.Cases.Update(caseEntity);
            await _context.SaveChangesAsync();
        }
    }
}
