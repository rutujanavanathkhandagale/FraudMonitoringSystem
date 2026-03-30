using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Investigator;
using FraudMonitoringSystem.Models.Investigator;
using System.Collections.Generic;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations.Investigator
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly WebContext _context;

        public TransactionRepository(WebContext context) => _context = context;

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction?> GetByIdAsync(int transactionId) =>
            await _context.Transactions.FindAsync(transactionId);

        public async Task<IEnumerable<Transaction>> GetAllAsync() =>
            await _context.Transactions.ToListAsync();

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task DeleteAsync(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public Task<Transaction> AddAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        // REMOVE all the bottom methods that threw NotImplementedException!
    }
}
