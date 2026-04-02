using FraudMonitoringSystem.Models;

namespace FraudMonitoringSystem.Interfaces
{
    public interface IControlChecklistRepository
    {
        Task<IEnumerable<ControlChecklist>> GetAllAsync();
        Task<IEnumerable<ControlChecklist>> GetByStatusAsync(string status);
        Task<ControlChecklist> GetByCaseIdAsync(int caseId);
        Task<bool> MasterCaseExistsAsync(int caseId);
        Task<ControlChecklist> AddAsync(ControlChecklist checklist);
        Task<ControlChecklist> UpdateAsync(ControlChecklist existing);
        Task<bool> DeleteAsync(int caseId);
    }
}