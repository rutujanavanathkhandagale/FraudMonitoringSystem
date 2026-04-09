using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.WatchList;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Watchlist;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations.Watchlist
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly WebContext _context;
        public WatchlistRepository(WebContext context) => _context = context;

        public async Task<IEnumerable<WatchlistEntry>> GetAllAsync() =>
            
            await _context.WatchlistEntries.ToListAsync();

        public async Task<WatchlistEntry?> GetByIdAsync(long id) =>
            await _context.WatchlistEntries.FindAsync(id);

        public async Task AddAsync(WatchlistEntry entry)
        {
            _context.WatchlistEntries.Add(entry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WatchlistEntry entry)
        {
            _context.WatchlistEntries.Update(entry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entry = await GetByIdAsync(id);
            if (entry != null)
            {
                _context.WatchlistEntries.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        //  Logic to match ACCOUNT number
        public async Task<WatchlistEntry?> GetByMatchIdentifierAsync(string identifier)

        {

            return await _context.WatchlistEntries

                .FirstOrDefaultAsync(w =>

                    w.Identifier.Trim().ToLower() == identifier.Trim().ToLower()
                   && w.Status == "Active");

        }

        public async Task<List<EntityLink>> GetByCustomerIdAsync(int customerId)

        {

            return await _context.EntityLinks

                .Where(x => x.FromCustomerId == customerId)

                .ToListAsync();

        }

    }
}