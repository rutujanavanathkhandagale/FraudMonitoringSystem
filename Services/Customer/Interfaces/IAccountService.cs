using FraudMonitoringSystem.Models.Customer;

namespace FraudMonitoringSystem.Services.Customer.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetAccountByIdAsync(string id);                // string instead of long
        Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(long customerId); // string instead of long
        Task<Account> CreateAccountAsync(Account account);
        Task<Account?> UpdateAccountAsync(Account account);
        Task<Account?> PatchAsync(string id, Account partialAccount); // string instead of long
        Task<bool> DeleteAccountAsync(string id);
    }
}
