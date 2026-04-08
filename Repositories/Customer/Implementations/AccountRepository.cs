using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Data;
using Microsoft.EntityFrameworkCore;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly WebContext _context;

        public AccountRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAsync() =>
            await _context.Accounts.Include(a => a.Customer).AsNoTracking().ToListAsync();

        public async Task<Account?> GetByIdAsync(string id) =>
            await _context.Accounts.Include(a => a.Customer)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(a => a.AccountId == id);

        public async Task<IEnumerable<Account>> GetByCustomerIdAsync(long customerId) =>
            await _context.Accounts.Include(a => a.Customer)
                                   .AsNoTracking()
                                   .Where(a => a.CustomerId == customerId)
                                   .ToListAsync();

        public async Task<int> GetAccountCountAsync()
        {
            return await _context.Accounts.CountAsync();
        }

        public async Task<Account> AddAsync(Account account)
        {
            // Generate next AccountId (ACC001 style)
            var lastId = await _context.Accounts
                .OrderByDescending(a => a.AccountId)
                .Select(a => a.AccountId)
                .FirstOrDefaultAsync();

            int nextNumber = lastId != null
                ? int.Parse(lastId.Replace("ACC", "")) + 1
                : 1;

            account.AccountId = $"ACC{nextNumber:D3}";

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account?> UpdateAsync(Account account)
        {
            var existing = await _context.Accounts.FindAsync(account.AccountId);
            if (existing == null) return null;

            existing.ProductType = account.ProductType;
            existing.Currency = account.Currency;
            existing.Status = account.Status;
            existing.AccountNumber = account.AccountNumber;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return false;

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
