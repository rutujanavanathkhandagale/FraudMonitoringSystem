using FraudMonitoringSystem.Models.ComplianceOfficer;
namespace FraudMonitoringSystem.Repositories.Customer.Interfaces
{
    public interface IPEPListRepository
    {
        Task<PEPListModel> AddAsync(PEPListModel model);
        Task<object> CheckPepAsync(long customerId);
    }
}