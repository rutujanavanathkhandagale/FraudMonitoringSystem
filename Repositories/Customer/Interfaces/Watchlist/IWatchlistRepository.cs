using FraudMonitoringSystem.Models.WatchList;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.Watchlist
{
    public interface IWatchlistRepository
    {
        Task<IEnumerable<WatchlistEntry>> GetAllAsync();
        Task<WatchlistEntry?> GetByIdAsync(long id);
        Task AddAsync(WatchlistEntry entry);
        Task UpdateAsync(WatchlistEntry entry);
        Task DeleteAsync(long id);

        //  Logic to match Customer identifier
        Task<List<EntityLink>> GetByCustomerIdAsync(int customerId);

        Task<WatchlistEntry?> GetByMatchIdentifierAsync(string identifier);
    }
}