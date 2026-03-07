using FraudMonitoringSystem.Models.ComplianceOfficer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;
namespace FraudMonitoringSystem.Services.Customer.Implementations.ComplianceOfficer
{
    public class SanctionService : ISanctionService
    {
        private readonly ISanctionRepository _repository;
        public SanctionService(ISanctionRepository repository)
        {
            _repository = repository;
        }
        public async Task<Sanction> AddAsync(Sanction model)
        {
            return await _repository.AddAsync(model);
        }
        public async Task<object> CheckSanctionAsync(long customerId)
        {
            return await _repository.CheckSanctionAsync(customerId);
        }
    }
}