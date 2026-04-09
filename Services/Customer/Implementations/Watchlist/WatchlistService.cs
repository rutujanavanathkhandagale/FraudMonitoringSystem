using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs.Watchlist;
using FraudMonitoringSystem.Models.WatchList;
using FraudMonitoringSystem.Repositories.Customer.Interfaces; // Ensure this points to your PersonalDetails Repo
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Watchlist;
using FraudMonitoringSystem.Services.Customer.Interfaces.Watchlist;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Watchlist
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository _watchlistRepo;
        private readonly IPersonalDetailsRepository _personalRepo;
        public readonly WebContext _context;

        public WatchlistService(IWatchlistRepository watchlistRepo, IPersonalDetailsRepository personalRepo,WebContext context)
        {
            _watchlistRepo = watchlistRepo;
            _personalRepo = personalRepo;
            _context = context;
        }

 
        public async Task<bool> IsCustomerOnWatchlistAsync(long customerId)

        {

            //Direct DB call 

            var account = await _context.Accounts

                .FirstOrDefaultAsync(a => a.CustomerId == customerId);

            if (account == null)

                return false;

            //Match with Watchlist

            var hit = await _watchlistRepo.GetByMatchIdentifierAsync(account.AccountNumber);

            return hit != null;

        }

        public async Task<WatchlistResponse> VerifyAsync(int customerId)

        {

   

            var hit = await _watchlistRepo.GetByCustomerIdAsync(customerId);

            if (hit != null && hit.Any())

            {

                return new WatchlistResponse

                {

                    CustomerId = customerId,

                    Status = "FAIL",

                    Message = "Customer is in watchlist (PEP/Sanction)."

                };

            }

            return new WatchlistResponse

            {

                CustomerId = customerId,

                Status = "PASS",

                Message = "Customer is clear of all watchlist flags."

            };

        }


        // EXISTING CRUD METHODS
        public async Task<IEnumerable<WatchlistEntry>> GetAllAsync() => await _watchlistRepo.GetAllAsync();
        public async Task<WatchlistEntry?> GetByIdAsync(long id) => await _watchlistRepo.GetByIdAsync(id);

        public async Task AddAsync(WatchlistEntryDto dto)
        {
            var entry = new WatchlistEntry
            {
                ListType = dto.ListType,
                Name = dto.Name,
                Identifier = dto.Identifier,
                Status = "Active"
            };
            await _watchlistRepo.AddAsync(entry);
        }

        public async Task UpdateAsync(WatchlistEntryDto dto)
        {
            var entry = await _watchlistRepo.GetByIdAsync(dto.EntryId);
            if (entry != null)
            {
                entry.Name = dto.Name;
                entry.Identifier = dto.Identifier;
                entry.ListType = dto.ListType;
                entry.Status = dto.Status;
                await _watchlistRepo.UpdateAsync(entry);
            }
        }

        public async Task DeleteAsync(long id) => await _watchlistRepo.DeleteAsync(id);
    }
}