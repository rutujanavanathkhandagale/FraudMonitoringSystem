using FraudMonitoringSystem.Interfaces;
using FraudMonitoringSystem.Models;

public class ControlChecklistService : IControlChecklistService
{
    private readonly IControlChecklistRepository _repo;
    public ControlChecklistService(IControlChecklistRepository repo)
    {
        _repo = repo;
    }

    public async Task<ControlChecklist> CreateAnalysisAsync(ControlChecklist checklist)
    {
        if (checklist.CaseID <= 0) throw new ArgumentException("CaseID must be a natural number.");
        if (!await _repo.MasterCaseExistsAsync(checklist.CaseID)) throw new KeyNotFoundException("CaseID not found.");

        checklist.OverallResult = Calculate(checklist.Details);
        return await _repo.AddAsync(checklist);
    }

    public async Task<ControlChecklist> UpdateAnalysisAsync(int caseId, List<ControlDetail> details)
    {
        var existing = await _repo.GetByCaseIdAsync(caseId);
        if (existing == null) return null;

        existing.Details = details;
        existing.OverallResult = Calculate(details);
        return await _repo.UpdateAsync(existing);
    }

    public async Task<IEnumerable<ControlChecklist>> GetHistoryAsync(string status) =>
        status == "ALL" ? await _repo.GetAllAsync() : await _repo.GetByStatusAsync(status);

    public async Task<bool> RemoveAnalysisAsync(int caseId) => await _repo.DeleteAsync(caseId);

    private string Calculate(List<ControlDetail> details) =>
        details.Any(d => d.Status == "PENDING") ? "PENDING" : (details.All(d => d.Status == "PASS") ? "PASS" : "FAIL");
}