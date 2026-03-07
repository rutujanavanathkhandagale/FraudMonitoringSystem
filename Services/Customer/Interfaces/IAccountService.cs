using FraudMonitoringSystem.Models.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudMonitoringSystem.Services.Customer.Interfaces
{
    public interface IAccountService
    {
        Task<int> CreateAccountAsync(Account account);
        Task<Account> PatchAsync(long id, Account partialAccount);
        Task<Account> GetAccountByIdAsync(long id);

    
        Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(long customerId);
    }
}