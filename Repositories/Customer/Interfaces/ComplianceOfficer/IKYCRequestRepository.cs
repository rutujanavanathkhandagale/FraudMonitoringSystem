using FraudMonitoringSystem.Models.Customer;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.ComplianceOfficer
{
    public interface IKYCRequestRepository
    {
        Task<KYCProfile?> GetByCustomerIdAsync(long customerId);
    }
}
