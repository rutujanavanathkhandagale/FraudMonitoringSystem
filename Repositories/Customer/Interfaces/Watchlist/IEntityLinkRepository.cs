using FraudMonitoringSystem.Models.WatchList;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.Watchlist
{
    public interface IEntityLinkRepository
    {
        Task<IEnumerable<EntityLink>> GetLinksByCustomerIdAsync(long customerId);
        Task<IEnumerable<EntityLink>> GetLinksByAccountIdAsync(long accountId);
        Task<int> AddLinkAsync(EntityLink link);
        Task<int> DeleteLinkAsync(long linkId);
    }
}