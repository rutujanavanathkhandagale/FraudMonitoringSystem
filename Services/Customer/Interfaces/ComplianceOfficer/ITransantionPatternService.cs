using FraudMonitoringSystem.Models.DTOs;


public interface ITransactionPatternService

{

    Task<TransactionPatternAnalysis> AnalyzeAsync(int customerId, int transactionID);

}
