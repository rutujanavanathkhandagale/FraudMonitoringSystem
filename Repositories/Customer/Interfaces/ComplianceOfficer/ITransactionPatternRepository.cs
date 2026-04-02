using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Models.Investigator;

public interface ITransactionPatternRepository
{
    Task<PersonalDetails> GetCustomerByIdAsync(int customerId);
    Task<List<Transaction>> GetTransactionsByCustomerIdAsync(int customerId);
    Task<int> GetMappedAlertCountAsync(int customerId);
    Task<Alert> GetHighestSeverityAlertAsync(int customerId);
    Task SaveRiskScoreAsync(RiskScore score);
}