using FraudMonitoringSystem.Models.AlertsCase;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase
{
	public interface IInvestigationNoteService
	{
		Task AddNote(InvestigationNote note);
		Task<IEnumerable<InvestigationNote>> GetNotesByCase(int caseId);
	}
}
