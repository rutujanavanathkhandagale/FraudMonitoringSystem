using FraudMonitoringSystem.Models.ComplianceOfficer;
using System.Collections.Generic;

using System.Threading.Tasks;

public interface IRegulatoryReportRepository

{

    Task<Regulatory_Report> CreateAsync(Regulatory_Report report);

    Task<List<Regulatory_Report>> GetAllAsync();

    Task<Regulatory_Report?> GetByIdAsync(int reportId);

    Task<List<Regulatory_Report>> GetByCaseIdAsync(int caseId);

    Task<Regulatory_Report?> UpdateAsync(Regulatory_Report report);
    // Add this to your interface
    Task<bool> FreezeCustomerAccountsByCaseIdAsync(int caseId);

    Task<bool> DeleteAsync(int reportId);
    Task<Regulatory_Report?> UpdateByCaseIdAsync(Regulatory_Report report);
    Task<bool> DeleteByCaseIdAsync(int caseId);
}
