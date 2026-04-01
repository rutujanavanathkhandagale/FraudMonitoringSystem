using FraudMonitoringSystem.Models.AlertsCase;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase
{
	public interface ICaseAttachmentRepository
	{
		Task AddAttachment(CaseAttachment attachment);

		Task<IEnumerable<CaseAttachment>> GetAttachmentsByCase(int caseId);
	}
}
