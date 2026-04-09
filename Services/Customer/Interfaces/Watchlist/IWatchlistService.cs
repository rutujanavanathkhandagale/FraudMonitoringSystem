using FraudMonitoringSystem.DTOs.Watchlist;
using FraudMonitoringSystem.Models.WatchList;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.Watchlist
{
    public interface IWatchlistService
    {
        // CRUD Operations
        Task<IEnumerable<WatchlistEntry>> GetAllAsync();
        Task<WatchlistEntry?> GetByIdAsync(long id);
        Task AddAsync(WatchlistEntryDto dto);
        Task UpdateAsync(WatchlistEntryDto dto);
        Task DeleteAsync(long id);

        // The Bridge between PersonalDetails and Watchlist
        Task<bool> IsCustomerOnWatchlistAsync(long customerId);
        Task<WatchlistResponse> VerifyAsync(int customerId);

    }
}