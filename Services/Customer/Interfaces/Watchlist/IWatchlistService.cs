using FraudMonitoringSystem.DTOs.Watchlist;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.Watchlist
{
    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistEntryDto>> GetAllAsync();
        Task<WatchlistEntryDto> GetByIdAsync(long id);
        Task AddAsync(WatchlistEntryDto dto);
        Task UpdateAsync(WatchlistEntryDto dto);
        Task DeleteAsync(long id);
    }

}
