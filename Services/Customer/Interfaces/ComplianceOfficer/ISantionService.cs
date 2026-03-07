using FraudMonitoringSystem.Models.ComplianceOfficer;
namespace FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer
{
    public interface ISanctionService
    {
        Task<Sanction> AddAsync(Sanction model);
        Task<object> CheckSanctionAsync(long customerId);
    }
}