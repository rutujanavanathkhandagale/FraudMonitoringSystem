using System.Threading.Tasks;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.ComplianceOfficer;
using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;
namespace FraudMonitoringSystem.Services.Customer.Implementations.ComplianceOfficer
{
    public class TransactionPatternService : ITransactionPatternService
    {
        private readonly ITransactionPatternRepository _repository;
        public TransactionPatternService(ITransactionPatternRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> CheckCustomerTransactionPattern(int customerId)
        {
            return await _repository.CheckCustomerTransactionPattern(customerId);
        }
    }
}