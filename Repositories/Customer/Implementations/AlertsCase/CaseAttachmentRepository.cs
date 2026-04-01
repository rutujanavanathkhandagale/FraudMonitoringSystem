using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Data;
using Microsoft.EntityFrameworkCore;


namespace FraudMonitoringSystem.Repositories.Customer.Implementations.AlertsCase
{
	public class CaseAttachmentRepository : ICaseAttachmentRepository
	{
		private readonly WebContext _context;

		public CaseAttachmentRepository(WebContext context)
		{
			_context = context;
		}

		public async Task AddAttachment(CaseAttachment attachment)
		{
			await _context.CaseAttachments.AddAsync(attachment);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<CaseAttachment>> GetAttachmentsByCase(int caseId)
		{
			return await _context.CaseAttachments
				.Where(x => x.CaseID == caseId)
				.ToListAsync();
		}
	}
}
