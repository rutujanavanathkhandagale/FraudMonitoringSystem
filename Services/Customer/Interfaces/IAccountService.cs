using FraudMonitoringSystem.Models.Customer;

namespace FraudMonitoringSystem.Services.Customer.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account> GetAccountByIdAsync(long id);
        Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(long customerId);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task<Account> PatchAsync(long id, Account partialAccount);
        Task DeleteAccountAsync(long id);
    }
}
