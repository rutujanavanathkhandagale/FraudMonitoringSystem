using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
namespace FraudMonitoringSystem.Services.Customer.Implementations.AlertsCase
{
	public class InvestigationNoteService : IInvestigationNoteService
	{
		private readonly IInvestigationNoteRepository _repository;

		public InvestigationNoteService(IInvestigationNoteRepository repository)
		{
			_repository = repository;
		}

		public async Task AddNote(InvestigationNote note)
		{
			await _repository.AddNote(note);
		}

		public async Task<IEnumerable<InvestigationNote>> GetNotesByCase(int caseId)
		{
			return await _repository.GetNotesByCase(caseId);
		}
	}
}
