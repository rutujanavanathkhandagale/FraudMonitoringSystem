using FraudMonitoringSystem.Models;
using System.Collections.Generic;

public class ControlChecklistService : IControlChecklistService
{
    private readonly IControlChecklistRepository _repository;
    public ControlChecklistService(IControlChecklistRepository repository)
    {
        _repository = repository;
    }
    public ControlChecklist Add(ControlChecklist checklist)
    {
        return _repository.Add(checklist);
    }
    public IEnumerable<ControlChecklist> GetAll()
    {
        return _repository.GetAll();
    }
    public ControlChecklist GetByCaseId(int caseId)
    {
        return _repository.GetByCaseId(caseId);
    }
    public IEnumerable<ControlChecklist> GetByStatus(string status)
    {
        return _repository.GetByStatus(status);
    }
    public ControlChecklist Update(ControlChecklist checklist)
    {
        return _repository.Update(checklist);
    }
    public bool Delete(int checklistId)
    {
        return _repository.Delete(checklistId);
    }
}