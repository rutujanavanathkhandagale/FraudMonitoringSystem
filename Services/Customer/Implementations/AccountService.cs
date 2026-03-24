using FraudMonitoringSystem.Exceptions;
using FraudMonitoringSystem.Exceptions.Admin;
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

        public async Task<Account> GetAccountByIdAsync(long id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null)
                throw new NotFoundException($"Account with ID {id} not found");
            return account;
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(long customerId)
        {
            var accounts = await _repository.GetByCustomerIdAsync(customerId);
            if (!accounts.Any())
                throw new NotFoundException($"No accounts found for Customer ID {customerId}");
            return accounts;
        }

        public async Task<Account> CreateAccountAsync(Account account) =>
            await _repository.AddAsync(account);

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            var updated = await _repository.UpdateAsync(account);
            if (updated == null)
                throw new NotFoundException($"Account with ID {account.AccountId} not found");
            return updated;
        }

        public async Task<Account> PatchAsync(long id, Account partialAccount)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException($"Account with ID {id} not found");

            if (!string.IsNullOrEmpty(partialAccount.ProductType))
                existing.ProductType = partialAccount.ProductType;
            if (!string.IsNullOrEmpty(partialAccount.Currency))
                existing.Currency = partialAccount.Currency;
            if (!string.IsNullOrEmpty(partialAccount.Status))
                existing.Status = partialAccount.Status;

            await _repository.UpdateAsync(existing);
            return existing;
        }

        public async Task DeleteAccountAsync(long id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                throw new NotFoundException($"Account with ID {id} not found");
        }
    }
}
