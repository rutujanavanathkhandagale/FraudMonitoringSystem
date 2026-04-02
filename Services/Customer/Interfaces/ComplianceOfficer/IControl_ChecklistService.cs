using FraudMonitoringSystem.Models;

namespace FraudMonitoringSystem.Interfaces
{
    public interface IControlChecklistService
    {
        Task<IEnumerable<ControlChecklist>> GetHistoryAsync(string status);
        Task<ControlChecklist> CreateAnalysisAsync(ControlChecklist checklist);
        Task<ControlChecklist> UpdateAnalysisAsync(int caseId, List<ControlDetail> details);
        Task<bool> RemoveAnalysisAsync(int caseId);
    }
}