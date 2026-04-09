using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.WatchList;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Watchlist;
using Microsoft.EntityFrameworkCore;


namespace FraudMonitoringSystem.Repositories.Customer.Implementations.Watchlist
{
    public class EntityLinkRepository : IEntityLinkRepository
    {
        private readonly WebContext _context;

        public EntityLinkRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EntityLink>> GetLinksByCustomerIdAsync(long customerId)
        {
            return await _context.EntityLinks
                .Where(l => l.FromCustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<EntityLink>> GetLinksByAccountIdAsync(long accountId)
        {
            return await _context.EntityLinks
                .Where(l => l.ToAccountId == accountId)
                .ToListAsync();
        }

        public async Task<int> AddLinkAsync(EntityLink link)
        {
            _context.EntityLinks.Add(link);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteLinkAsync(long linkId)
        {
            var link = await _context.EntityLinks.FindAsync(linkId);
            if (link != null)
            {
                _context.EntityLinks.Remove(link);
                return await _context.SaveChangesAsync();
            }
            return 0; // Service layer will throw NotFoundException
        }
    }
}