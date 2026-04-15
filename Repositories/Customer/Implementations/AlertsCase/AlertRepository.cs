using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using Microsoft.EntityFrameworkCore;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Data;
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

		public async Task<IEnumerable<Alert>> GetAllAlerts()
		{
			return await _context.Alerts.ToListAsync();
		}

		public async Task<Alert> GetAlertById(int id)
		{
			return await _context.Alerts.FindAsync(id);
		}

		public async Task AddAlert(Alert alert)
		{
			await _context.Alerts.AddAsync(alert);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAlert(Alert alert)
		{
			_context.Alerts.Update(alert);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAlert(int id)
		{
			var alert = await _context.Alerts.FindAsync(id);

			if (alert != null)
			{
				// 🔥 DELETE RELATED CASES FIRST
				var relatedCases = _context.Cases
					.Where(c => c.AlertId == id);

				_context.Cases.RemoveRange(relatedCases);

				// 🔥 DELETE ALERT
				_context.Alerts.Remove(alert);

				await _context.SaveChangesAsync();
			}
		}


		public Task AddAlert(object alert)
		{
			throw new NotImplementedException();
		}
	}
}


