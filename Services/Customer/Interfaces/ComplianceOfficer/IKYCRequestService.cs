using FraudMonitoringSystem.Models.Customer;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer
{
    public interface IKYCRequestService
    {
        Task<KYCProfile?> GetByCustomerIdAsync(long customerId);
    }
}
