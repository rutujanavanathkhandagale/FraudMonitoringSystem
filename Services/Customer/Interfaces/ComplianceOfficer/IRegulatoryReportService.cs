using FraudMonitoringSystem.Models.ComplianceOfficer;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRegulatoryReportService
{
    Regulatory_Report GenerateReport(int customerId);
    IEnumerable<Regulatory_Report> GetByCustomerId(int customerId);
    IEnumerable<Regulatory_Report> GetByStatus(string status);
    Regulatory_Report UpdateReport(int id, Regulatory_Report report);
    bool DeleteReport(int id);
}