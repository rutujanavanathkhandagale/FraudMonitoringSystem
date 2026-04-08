using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using FraudMonitoringSystem.Services.Customer.Interfaces;

namespace FraudMonitoringSystem.Services.Customer.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Account>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Account?> GetAccountByIdAsync(string id)
        {
            var account = await _repository.GetByIdAsync(id);
            return account; // no exception, just return null if not found
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(long customerId)
        {
            var accounts = await _repository.GetByCustomerIdAsync(customerId);
            return accounts; // no exception, just return empty list if none
        }

        // Generate AccountId before saving
        public async Task<Account> CreateAccountAsync(Account account)
        {
            account.AccountId = GenerateAccountId();
            return await _repository.AddAsync(account);
        }

        private string GenerateAccountId()
        {
            return $"ACC{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }

        public async Task<Account?> UpdateAccountAsync(Account account)
        {
            var updated = await _repository.UpdateAsync(account);
            return updated; // no exception, just return null if not found
        }

        public async Task<Account?> PatchAsync(string? id, Account partialAccount)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return null; // no exception, just return null

            if (!string.IsNullOrEmpty(partialAccount.ProductType))
                existing.ProductType = partialAccount.ProductType;
            if (!string.IsNullOrEmpty(partialAccount.Currency))
                existing.Currency = partialAccount.Currency;
            if (!string.IsNullOrEmpty(partialAccount.Status))
                existing.Status = partialAccount.Status;
            if (!string.IsNullOrEmpty(partialAccount.AccountNumber))
                existing.AccountNumber = partialAccount.AccountNumber;

            await _repository.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAccountAsync(string id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted; // no exception, just return false if not found
        }
    }
}
