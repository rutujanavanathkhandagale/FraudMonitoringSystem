using FraudMonitoringSystem.Models.Investigator;


namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.Investigator
{
    public interface ITransactionRepository
    {
        // Add the question mark here: Task<Transaction?>
        Task<Transaction?> GetByIdAsync(int transactionId);

        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> AddAsync(Transaction transaction);
        Task<Transaction> UpdateAsync(Transaction transaction);
        Task DeleteAsync(int transactionId);
        Task<Transaction> AddTransactionAsync(Transaction transaction);
    }
}

