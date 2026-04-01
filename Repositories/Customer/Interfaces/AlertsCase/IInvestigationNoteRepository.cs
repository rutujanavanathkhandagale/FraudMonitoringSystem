using FraudMonitoringSystem.Models.AlertsCase;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase
{
	public interface IInvestigationNoteRepository
	{
		Task AddNote(InvestigationNote note);

		Task<IEnumerable<InvestigationNote>> GetNotesByCase(int caseId);
	}
}
