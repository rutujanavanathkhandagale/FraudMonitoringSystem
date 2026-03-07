using FraudMonitoringSystem.Models.ComplianceOfficer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;
namespace FraudMonitoringSystem.Services.Customer.Implementations.ComplianceOfficer
{
    public class PEPListService : IPEPListService
    {
        private readonly IPEPListRepository _repository;
        public PEPListService(IPEPListRepository repository)
        {
            _repository = repository;
        }
        public async Task<PEPListModel> AddAsync(PEPListModel model)
        {
            return await _repository.AddAsync(model);
        }
        public async Task<object> CheckPepAsync(long customerId)
        {
            return await _repository.CheckPepAsync(customerId);
        }
    }
}