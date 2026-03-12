using FraudMonitoringSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IControlChecklistRepository
{
    Task<ControlChecklist> ExecuteChecklist(int caseId, string checkedBy);
    Task<List<ControlChecklist>> GetAllAsync();
    Task<List<ControlChecklist>> GetByResultAsync(string result);
}