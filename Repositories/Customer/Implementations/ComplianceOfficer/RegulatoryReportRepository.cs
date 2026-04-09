using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.ComplianceOfficer;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using Microsoft.EntityFrameworkCore;


public class RegulatoryReportRepository : IRegulatoryReportRepository

{

    private readonly WebContext _context;

    public RegulatoryReportRepository(WebContext context)

    {

        _context = context;

    }

    public async Task<Regulatory_Report> CreateAsync(Regulatory_Report report)

    {

        _context.Regulatory_Report.Add(report);

        await _context.SaveChangesAsync();

        return report;

    }

    public async Task<List<Regulatory_Report>> GetAllAsync()

    {

        return await _context.Regulatory_Report.ToListAsync();

    }

    public async Task<Regulatory_Report?> GetByIdAsync(int reportId)

    {

        return await _context.Regulatory_Report.FindAsync(reportId);

    }

    public async Task<List<Regulatory_Report>> GetByCaseIdAsync(int caseId)

    {

        return await _context.Regulatory_Report

            .Where(r => r.CaseID == caseId)

            .ToListAsync();

    }


    public async Task<Regulatory_Report?> UpdateAsync(Regulatory_Report report)

    {

        var existing = await _context.Regulatory_Report.FindAsync(report.ReportID);

        if (existing == null) return null;

        existing.ReportType = report.ReportType;

        existing.Period = report.Period;

        existing.SubmittedDate = report.SubmittedDate;

        existing.Status = report.Status;

        await _context.SaveChangesAsync();

        return existing;

    }

    public async Task<bool> DeleteAsync(int reportId)

    {

        var report = await _context.Regulatory_Report.FindAsync(reportId);

        if (report == null) return false;

        _context.Regulatory_Report.Remove(report);

        await _context.SaveChangesAsync();

        return true;

    }
    public async Task<bool> FreezeCustomerAccountsByCaseIdAsync(int caseId)
    {
        //Find the Case first
        var alertCase = await _context.Cases.FirstOrDefaultAsync(c => c.CaseID == caseId);

        if (alertCase == null)
        {
            Console.WriteLine($"DEBUG: Case {caseId} not found in database.");
            return false;
        }

        // Use the CustomerId from that case to find accounts
        var accounts = await _context.Accounts
            .Where(a => a.CustomerId == alertCase.CustomerId)
            .ToListAsync();

        if (accounts.Count == 0)
        {
            Console.WriteLine($"DEBUG: No accounts found for Customer {alertCase.CustomerId}.");
            return false;
        }

        // Freeze them
        foreach (var acc in accounts)
        {
            acc.Status = "Frozen";
        }

        await _context.SaveChangesAsync();
        return true;
    }
    // UPDATE BY CASE ID
    public async Task<Regulatory_Report?> UpdateByCaseIdAsync(Regulatory_Report report)
    {
        var existing = await _context.Regulatory_Report
            .FirstOrDefaultAsync(r => r.CaseID == report.CaseID);

        if (existing == null) return null;

        existing.ReportType = report.ReportType;
        existing.Period = report.Period;
        existing.Status = report.Status;
        existing.SubmittedDate = report.SubmittedDate;

        await _context.SaveChangesAsync();
        return existing;
    }

    // DELETE BY CASE ID
    public async Task<bool> DeleteByCaseIdAsync(int caseId)
    {
        var report = await _context.Regulatory_Report
            .FirstOrDefaultAsync(r => r.CaseID == caseId);

        if (report == null) return false;

        _context.Regulatory_Report.Remove(report);
        await _context.SaveChangesAsync();
        return true;
    }

}
 

