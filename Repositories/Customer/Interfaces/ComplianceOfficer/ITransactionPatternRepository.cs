using System.Threading.Tasks;
namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.ComplianceOfficer
{
    public interface ITransactionPatternRepository
    {
        Task<string> CheckCustomerTransactionPattern(int customerId);
    }
}