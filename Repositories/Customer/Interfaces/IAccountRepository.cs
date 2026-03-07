using FraudMonitoringSystem.Models.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces
{
    public interface IAccountRepository
    {
        Task<int> AddAsync(Account account);
        Task<Account> PatchAsync(Account account);
        Task<Account> GetByIdAsync(long id);

        // ✅ New: fetch accounts by CustomerId
        Task<IEnumerable<Account>> GetByCustomerIdAsync(long customerId);
    }
}