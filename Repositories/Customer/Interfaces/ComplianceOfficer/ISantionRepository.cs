using FraudMonitoringSystem.Models.ComplianceOfficer;
namespace FraudMonitoringSystem.Repositories.Customer.Interfaces
{
    public interface ISanctionRepository
    {
        Task<Sanction> AddAsync(Sanction model);
        Task<object> CheckSanctionAsync(long customerId);
    }
}