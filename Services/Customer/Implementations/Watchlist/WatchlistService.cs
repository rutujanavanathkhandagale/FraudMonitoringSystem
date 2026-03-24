using FraudMonitoringSystem.DTOs.Watchlist;
using FraudMonitoringSystem.Exceptions.Admin;
using FraudMonitoringSystem.Models.WatchList;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Watchlist;
using FraudMonitoringSystem.Services.Customer.Interfaces.Watchlist;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Watchlist
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository _repo;

        public WatchlistService(IWatchlistRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<WatchlistEntryDto>> GetAllAsync()
        {
            var entries = await _repo.GetAllAsync();
            if (!entries.Any()) throw new NotFoundException("No watchlist entries found");
            return entries.Select(e => new WatchlistEntryDto
            {
                EntryId = e.EntryId,
                ListType = e.ListType,
                Name = e.Name,
                Identifier = e.Identifier,
                Status = e.Status
            });
        }

        public async Task<WatchlistEntryDto> GetByIdAsync(long id)
        {
            var entry = await _repo.GetByIdAsync(id);
            if (entry == null) throw new NotFoundException($"Watchlist entry {id} not found");
            return new WatchlistEntryDto
            {
                EntryId = entry.EntryId,
                ListType = entry.ListType,
                Name = entry.Name,
                Identifier = entry.Identifier,
                Status = entry.Status
            };
        }

        public async Task AddAsync(WatchlistEntryDto dto)
        {
            var entry = new WatchlistEntry
            {
                ListType = dto.ListType,
                Name = dto.Name,
                Identifier = dto.Identifier,
                Status = dto.Status
            };
            await _repo.AddAsync(entry);
        }

        public async Task UpdateAsync(WatchlistEntryDto dto)
        {
            var entry = await _repo.GetByIdAsync(dto.EntryId);
            if (entry == null) throw new NotFoundException($"Watchlist entry {dto.EntryId} not found");
            entry.ListType = dto.ListType;
            entry.Name = dto.Name;
            entry.Identifier = dto.Identifier;
            entry.Status = dto.Status;
            await _repo.UpdateAsync(entry);
        }

        public async Task DeleteAsync(long id)
        {
            var entry = await _repo.GetByIdAsync(id);
            if (entry == null) throw new NotFoundException($"Watchlist entry {id} not found");
            await _repo.DeleteAsync(id);
        }
    }

}
