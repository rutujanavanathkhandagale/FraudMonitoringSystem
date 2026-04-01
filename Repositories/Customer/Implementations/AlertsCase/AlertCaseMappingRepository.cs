using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations.AlertsCase
{
	public class AlertCaseMappingRepository : IAlertCaseMappingRepository
	{
		private readonly WebContext _context;

		public AlertCaseMappingRepository(WebContext context)
		{
			_context = context;
		}

		public async Task AddMapping(AlertCaseMapping mapping)
		{
			await _context.AlertCaseMappings.AddAsync(mapping);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<AlertCaseMapping>> GetMappingsByCase(int caseId)
		{
			return await _context.AlertCaseMappings
				.Where(x => x.CaseID == caseId)
				.ToListAsync();
		}
	}
}
