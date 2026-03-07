using FraudMonitoringSystem.Models;
using System.Collections.Generic;
public interface IControlChecklistService
{
    ControlChecklist Add(ControlChecklist checklist);
    IEnumerable<ControlChecklist> GetAll();
    ControlChecklist GetByCaseId(int caseId);
    IEnumerable<ControlChecklist> GetByStatus(string status);
    ControlChecklist Update(ControlChecklist checklist);
    bool Delete(int checklistId);
}