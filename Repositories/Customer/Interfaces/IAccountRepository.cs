using FraudMonitoringSystem.Models.Customer;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(string id);              // changed to string
        Task<IEnumerable<Account>> GetByCustomerIdAsync(long customerId); // changed to string
        Task<Account> AddAsync(Account account);
        Task<Account?> UpdateAsync(Account account);
        Task<bool> DeleteAsync(string id);                   // changed to string
    }
}
