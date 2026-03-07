using FraudMonitoringSystem.Models.ComplianceOfficer;
namespace FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer
{
    public interface IPEPListService
    {
        Task<PEPListModel> AddAsync(PEPListModel model);
        Task<object> CheckPepAsync(long customerId);
    }
}