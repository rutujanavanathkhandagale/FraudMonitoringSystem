using System.Collections.Generic;
using System.Threading.Tasks;
using FraudMonitoringSystem.DTOs.ComplianceOfficer;
using FraudMonitoringSystem.Models.ComplianceOfficer;

public interface IRegulatoryReportService

{

    Task<Regulatory_Report> CreateAsync(CreateReportDto dto);

    Task<List<Regulatory_Report>> GetAllAsync();

    Task<Regulatory_Report?> GetByIdAsync(int id);

    Task<List<Regulatory_Report>> GetByCaseIdAsync(int caseId);

    Task<Regulatory_Report?> UpdateAsync(int id, RegulatoryReportUpdateDto dto);

    Task<bool> DeleteAsync(int id);
    Task<Regulatory_Report?> UpdateByCaseIdAsync(int caseId, RegulatoryReportUpdateDto dto);
    Task<bool> DeleteByCaseIdAsync(int caseId);

}
