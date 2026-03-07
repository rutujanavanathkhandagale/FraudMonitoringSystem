using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.ComplianceOfficer;
using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;
public class KYCRequestService : IKYCRequestService
{
    private readonly IKYCRequestRepository _repository;
    public KYCRequestService(IKYCRequestRepository repository)
    {
        _repository = repository;
    }
    public async Task<KYCProfile?> GetByCustomerIdAsync(long customerId)
    {
        return await _repository.GetByCustomerIdAsync(customerId);
    }
}