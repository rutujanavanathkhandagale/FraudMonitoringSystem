using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
namespace FraudMonitoringSystem.Services.Customer.Implementations.AlertsCase
{
	public class CaseAttachmentService : ICaseAttachmentService
	{
		private readonly ICaseAttachmentRepository _repository;

		public CaseAttachmentService(ICaseAttachmentRepository repository)
		{
			_repository = repository;
		}

		public async Task AddAttachment(CaseAttachment attachment)
		{
			await _repository.AddAttachment(attachment);
		}

		public async Task<IEnumerable<CaseAttachment>> GetAttachmentsByCase(int caseId)
		{
			return await _repository.GetAttachmentsByCase(caseId);
		}
	}
}
