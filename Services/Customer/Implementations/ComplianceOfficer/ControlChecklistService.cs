using FraudMonitoringSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
public class ControlChecklistService : IControlChecklistService
{
    private readonly IControlChecklistRepository _repository;
    public ControlChecklistService(IControlChecklistRepository repository)
    {
        _repository = repository;
    }
    public async Task<ControlChecklist> ExecuteChecklist(int caseId, string checkedBy)
    {
        return await _repository.ExecuteChecklist(caseId, checkedBy);
    }
    public async Task<List<ControlChecklist>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<List<ControlChecklist>> GetByResultAsync(string result)
    {
        return await _repository.GetByResultAsync(result);
    }
}