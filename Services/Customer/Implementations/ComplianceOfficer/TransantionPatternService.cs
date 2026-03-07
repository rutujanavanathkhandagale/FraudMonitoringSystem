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
        public string CheckCustomerTransactionPattern(int customerId)
        {
            return _repository.CheckCustomerTransactionPattern(customerId);
        }
    }
}
