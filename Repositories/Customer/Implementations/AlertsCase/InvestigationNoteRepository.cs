using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.Data;
using Microsoft.EntityFrameworkCore;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations.AlertsCase
{
	public class InvestigationNoteRepository : IInvestigationNoteRepository
	{
		private readonly WebContext _context;

		public InvestigationNoteRepository(WebContext context)
		{
			_context = context;
		}

		public async Task AddNote(InvestigationNote note)
		{
			await _context.InvestigationNotes.AddAsync(note);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<InvestigationNote>> GetNotesByCase(int caseId)
		{
			return await _context.InvestigationNotes
				.Where(x => x.CaseID == caseId)
				.ToListAsync();
		}
	}
}
