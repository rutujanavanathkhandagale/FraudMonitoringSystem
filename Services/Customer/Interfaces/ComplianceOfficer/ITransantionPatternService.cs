using FraudMonitoringSystem.Models.DTOs;

public interface ITransactionPatternService
{
    Task<TransactionPatternAnalysis> AnalyzePatternAsync(int customerId);
}