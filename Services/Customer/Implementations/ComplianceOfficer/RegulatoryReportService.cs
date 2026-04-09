
using FraudMonitoringSystem.DTOs.ComplianceOfficer;
using FraudMonitoringSystem.Models.ComplianceOfficer;

public class RegulatoryReportService : IRegulatoryReportService
{
    private readonly IRegulatoryReportRepository _repository;

    public RegulatoryReportService(IRegulatoryReportRepository repository)
    {
        _repository = repository;
    }

    //Now triggers account freeze if status is "Submitted"
    public async Task<Regulatory_Report> CreateAsync(CreateReportDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        var report = new Regulatory_Report
        {
            CaseID = dto.CaseID,
            ReportType = dto.ReportType,
            Period = dto.Period,
            SubmittedDate = dto.SubmittedDate,
            Status = dto.Status
        };

        var createdReport = await _repository.CreateAsync(report);

        // Check if the report is submitted  freeze the customer's accounts
        if (createdReport.Status.Equals("Submitted", StringComparison.OrdinalIgnoreCase))
        {
            await _repository.FreezeCustomerAccountsByCaseIdAsync(createdReport.CaseID);
        }

        return createdReport;
    }

    // GET ALL
    public async Task<List<Regulatory_Report>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    // GET BY ID
    public async Task<Regulatory_Report?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid Report ID");

        return await _repository.GetByIdAsync(id);
    }

    //GET BY CASE ID
    public async Task<List<Regulatory_Report>> GetByCaseIdAsync(int caseId)
    {
        if (caseId <= 0)
            throw new ArgumentException("Invalid Case ID");

        return await _repository.GetByCaseIdAsync(caseId);
    }

    // UPDATE Also triggers account freeze if status changes to "Submitted"
    public async Task<Regulatory_Report?> UpdateAsync(int id, RegulatoryReportUpdateDto dto)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid Report ID");

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        // Update fields
        existing.ReportType = dto.ReportType;
        existing.Status = dto.Status;

        var updatedReport = await _repository.UpdateAsync(existing);

        // If the update sets the status to Submitted, freeze the accounts
        if (updatedReport != null && updatedReport.Status.Equals("Submitted", StringComparison.OrdinalIgnoreCase))
        {
            await _repository.FreezeCustomerAccountsByCaseIdAsync(updatedReport.CaseID);
        }

        return updatedReport;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid Report ID");

        return await _repository.DeleteAsync(id);
    }
    // UPDATE BY CASE ID
    public async Task<Regulatory_Report?> UpdateByCaseIdAsync(int caseId, RegulatoryReportUpdateDto dto)
    {
        if (caseId <= 0) throw new ArgumentException("Invalid Case ID");

        var reports = await _repository.GetByCaseIdAsync(caseId);
        var existing = reports.FirstOrDefault();

        if (existing == null) return null;

        existing.ReportType = dto.ReportType;
        existing.Status = dto.Status;

        var updatedReport = await _repository.UpdateByCaseIdAsync(existing);

        if (updatedReport != null && updatedReport.Status.Equals("Submitted", StringComparison.OrdinalIgnoreCase))
        {
            await _repository.FreezeCustomerAccountsByCaseIdAsync(updatedReport.CaseID);
        }

        return updatedReport;
    }

    // DELETE BY CASE ID
    public async Task<bool> DeleteByCaseIdAsync(int caseId)
    {
        if (caseId <= 0) throw new ArgumentException("Invalid Case ID");
        return await _repository.DeleteByCaseIdAsync(caseId);
    }
}