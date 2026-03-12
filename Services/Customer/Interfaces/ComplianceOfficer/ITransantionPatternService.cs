using System.Threading.Tasks;
namespace FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer
{
    public interface ITransactionPatternService
    {
        Task<string> CheckCustomerTransactionPattern(int customerId);
    }
}