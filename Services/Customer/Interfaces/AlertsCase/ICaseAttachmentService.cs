using FraudMonitoringSystem.Models.AlertsCase;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase
{
	public interface ICaseAttachmentService
	{
		Task AddAttachment(CaseAttachment attachment);
		Task<IEnumerable<CaseAttachment>> GetAttachmentsByCase(int caseId);
	}
}
