using FraudMonitoringSystem.Models.Customer;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(long id);
        Task<IEnumerable<Account>> GetByCustomerIdAsync(long customerId);
        Task<Account> AddAsync(Account account);
        Task<Account> UpdateAsync(Account account);
        Task<bool> DeleteAsync(long id);
    }
}
