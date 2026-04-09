using FraudMonitoringSystem.Models;

public interface IControlChecklistService

{
    


    Task<ControlChecklist> CreateAnalysisAsync(ControlChecklist checklist);

    Task<ControlChecklist> UpdateAnalysisAsync(int caseId, List<ControlDetail> details);

    Task<IEnumerable<ControlChecklist>> GetHistoryAsync(string status);

    Task<bool> RemoveAnalysisAsync(int caseId);

}
