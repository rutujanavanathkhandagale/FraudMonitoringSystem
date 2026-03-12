using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.ComplianceOfficer;
using FraudMonitoringSystem.Services.Customer.Interfaces;

public class RegulatoryReportRepository : IRegulatoryReportRepository

{

    private readonly WebContext _context;

    public RegulatoryReportRepository(WebContext context)

    {

        _context = context;

    }

    public Regulatory_Report GenerateReport(int customerId)

    {

        var caseData = _context.Cases

            .FirstOrDefault(c => c.CustomerId == customerId);

        if (caseData == null)

            throw new Exception("Case not found");

        var checklist = _context.Control_Checklist

            .FirstOrDefault(c => c.CaseID == caseData.CaseID);

        var transaction = _context.Transaction

            .FirstOrDefault(t => t.CustomerId == customerId);

        string reportType = "SAR";

        string status = "Fail";

        // Currency Logic

        if (transaction != null && transaction.Currency == "INR")

            reportType = "STR";

        else

            reportType = "SAR";

        // Control Checklist Logic

        if (checklist != null && checklist.Result == "Pass")

            status = "Pass";

        // Trusted Source Logic

        var trustedSources = new List<string>

        {

            "LIC",

            "Bank",

            "Scholarship",

            "Government",

            "IncomeTax"

        };

        if (transaction != null && trustedSources.Contains(transaction.SourceType))

            status = "Pass";

        var report = new Regulatory_Report

        {

            CaseID = caseData.CaseID,

            ReportType = reportType,

            Period = DateTime.Now.ToString("yyyy-MM"),

            SubmittedDate = DateTime.Now,

            Status = status

        };

        _context.Regulatory_Report.Add(report);

        _context.SaveChanges();

        return report;

    }

    public IEnumerable<Regulatory_Report> GetByCustomerId(int customerId)

    {

        return _context.Regulatory_Report

            .Where(r => r.Case.CustomerId == customerId)

            .ToList();

    }

    public IEnumerable<Regulatory_Report> GetByStatus(string status)

    {

        return _context.Regulatory_Report

            .Where(r => r.Status == status)

            .ToList();

    }

    public Regulatory_Report UpdateReport(int id, Regulatory_Report report)

    {

        var existing = _context.Regulatory_Report

            .FirstOrDefault(r => r.ReportID == id);

        if (existing == null)

            throw new Exception("Report not found");

        existing.Status = report.Status;

        existing.ReportType = report.ReportType;

        _context.SaveChanges();

        return existing;

    }

    public bool DeleteReport(int id)

    {

        var report = _context.Regulatory_Report

            .FirstOrDefault(r => r.ReportID == id);

        if (report == null)

            return false;

        _context.Regulatory_Report.Remove(report);

        _context.SaveChanges();

        return true;

    }

}
