using FraudMonitoringSystem.Data;

using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using Microsoft.EntityFrameworkCore;





namespace FraudMonitoringSystem.Repositories.Customer.Implementations.AlertsCase
{
    public class AlertRepository : IAlertRepository
    {

        private readonly WebContext _context;

        public AlertRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<Alert> CreateAlertAsync(Alert alert)
        {
            await _context.Alerts.AddAsync(alert);
            await _context.SaveChangesAsync();
            return alert;
        }

        public async Task<List<Alert>> GetAllAlertsAsync()
        {
            return await _context.Alerts
                .Include(a => a.Case)
                .Include(a => a.DetectionRule)
                .ThenInclude(d => d.Scenario)
                .ToListAsync();
        }

        public async Task<Alert?> GetAlertByIdAsync(int id)
        {
            return await _context.Alerts
                .Include(a => a.Case)
                .FirstOrDefaultAsync(a => a.AlertID == id);
        }

        public async Task UpdateAlertAsync(Alert alert)
        {
            _context.Alerts.Update(alert);
            await _context.SaveChangesAsync();
        }
    }
}
