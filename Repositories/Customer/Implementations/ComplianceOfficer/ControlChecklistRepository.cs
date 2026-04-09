using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models;
using Microsoft.EntityFrameworkCore;

public class ControlChecklistRepository : IControlChecklistRepository

{

    private readonly WebContext _context;

    public ControlChecklistRepository(WebContext context) => _context = context;

    public async Task<IEnumerable<ControlChecklist>> GetAllAsync() =>

         await _context.Control_Checklist.Include(c => c.Details).AsNoTracking().ToListAsync();

    public async Task<IEnumerable<ControlChecklist>> GetByStatusAsync(string status) =>

        await _context.Control_Checklist.Include(c => c.Details)

            .Where(x => x.OverallResult == status).AsNoTracking().ToListAsync();

    public async Task<ControlChecklist> GetByCaseIdAsync(int caseId) =>

        await _context.Control_Checklist.Include(c => c.Details)

            .FirstOrDefaultAsync(x => x.CaseID == caseId);
    public async Task<int> GetTransactionIdByCaseIdAsync(int caseId)

    {

        return await _context.Cases

            .Where(c => c.CaseID == caseId)

            .Select(c => c.TransactionId)

            .FirstOrDefaultAsync();

    }


    public async Task<bool> MasterCaseExistsAsync(int caseId) =>

        await _context.Cases.AnyAsync(c => c.CaseID == caseId);

  
    public async Task<int> GetCustomerIdByCaseIdAsync(int caseId)

    {

            var customerId= await _context.Cases

            .Where(c => c.CaseID == caseId)

            .Select(c => c.CustomerId)

            .FirstOrDefaultAsync();
        return (int)customerId;
       

    }


    public async Task<ControlChecklist> AddAsync(ControlChecklist checklist)

    {

        _context.Control_Checklist.Add(checklist);

        await _context.SaveChangesAsync();

        return checklist;

    }

    public async Task<ControlChecklist> UpdateAsync(ControlChecklist existing)

    {

        _context.Control_Checklist.Update(existing);

        await _context.SaveChangesAsync();

        return existing;

    }

    public async Task<bool> DeleteAsync(int caseId)

    {

        var record = await _context.Control_Checklist.FirstOrDefaultAsync(x => x.CaseID == caseId);

        if (record == null) return false;

        _context.Control_Checklist.Remove(record);

        await _context.SaveChangesAsync();

        return true;

    }

}
