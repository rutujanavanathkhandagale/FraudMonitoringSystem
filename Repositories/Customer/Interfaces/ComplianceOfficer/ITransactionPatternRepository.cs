using FraudMonitoringSystem.Models.Investigator;

public interface ITransactionPatternRepository

{

    Task<Transaction> GetTransactionByIdAsync(int transactionID);

    Task<List<Transaction>> GetTransactionsByCustomerIdAsync(int customerId);

    Task<RiskScore> GetRiskScoreByTransactionIdAsync(int transactionID);

}
