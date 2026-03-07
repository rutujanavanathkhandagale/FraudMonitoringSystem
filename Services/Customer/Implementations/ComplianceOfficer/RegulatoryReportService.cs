using FraudMonitoringSystem.Models.ComplianceOfficer;
using System.Collections.Generic;
using System.Threading.Tasks;
public class RegulatoryReportService : IRegulatoryReportService
{
    private readonly IRegulatoryReportRepository _repository;
    public RegulatoryReportService(IRegulatoryReportRepository repository)
    {
        _repository = repository;
    }
    public Regulatory_Report GenerateReport(int customerId)
    {
        return _repository.GenerateReport(customerId);
    }
    public IEnumerable<Regulatory_Report> GetByCustomerId(int customerId)
    {
        return _repository.GetByCustomerId(customerId);
    }
    public IEnumerable<Regulatory_Report> GetByStatus(string status)
    {
        return _repository.GetByStatus(status);
    }
    public Regulatory_Report UpdateReport(int id, Regulatory_Report report)
    {
        return _repository.UpdateReport(id, report);
    }
    public bool DeleteReport(int id)
    {
        return _repository.DeleteReport(id);
    }
}