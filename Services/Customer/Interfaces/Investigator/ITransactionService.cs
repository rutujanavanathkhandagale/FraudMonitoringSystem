using FraudMonitoringSystem.DTOs.Investigator;
using FraudMonitoringSystem.Models.Investigator; 

namespace FraudMonitoringSystem.Services.Customer.Interfaces.Investigator
{
    public interface ITransactionService
    {
        Task<TransactionDto> AddTransactionAsync(TransactionDto dto);
        Task<TransactionDto> GetByIdAsync(int transactionId);
        Task<IEnumerable<TransactionDto>> GetAllAsync();
        Task<TransactionDto> UpdateAsync(TransactionDto dto);
        Task DeleteAsync(int transactionId);
    }
}
