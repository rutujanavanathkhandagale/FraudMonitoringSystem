using FraudMonitoringSystem.Data;

using FraudMonitoringSystem.Models;

using System;

using System.Collections.Generic;

using System.Linq;

namespace FraudMonitoringSystem.Repositories

{
    public class ControlChecklistRepository : IControlChecklistRepository
    {
        private readonly WebContext _context;
        public ControlChecklistRepository(WebContext context)
        {
            _context = context;
        }
        public ControlChecklist Add(ControlChecklist checklist)
        {
            checklist.CheckedDate = DateTime.Now;
            _context.Control_Checklist.Add(checklist);
            _context.SaveChanges();
            return checklist;
        }
        public IEnumerable<ControlChecklist> GetAll()
        {
            return _context.Control_Checklist.ToList();
        }
        public ControlChecklist GetByCaseId(int caseId)
        {
            return _context.Control_Checklist
                .FirstOrDefault(c => c.CaseID == caseId);
        }
        public IEnumerable<ControlChecklist> GetByStatus(string status)
        {
            return _context.Control_Checklist
                .Where(c => c.Status == status)
                .ToList();
        }
        public ControlChecklist Update(ControlChecklist checklist)
        {
            var existing = _context.Control_Checklist
                .FirstOrDefault(c => c.ChecklistID == checklist.ChecklistID);
            if (existing == null)
                return null;
            existing.CheckedBy = checklist.CheckedBy;
            existing.CheckedDate = checklist.CheckedDate;
            existing.Status = checklist.Status;
            existing.Result = checklist.Result;
            _context.SaveChanges();
            return existing;
        }
        public bool Delete(int checklistId)
        {
            var entity = _context.Control_Checklist
                .FirstOrDefault(c => c.ChecklistID == checklistId);
            if (entity == null)
                return false;
            _context.Control_Checklist.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}