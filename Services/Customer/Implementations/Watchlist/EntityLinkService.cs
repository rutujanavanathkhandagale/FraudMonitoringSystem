namespace FraudMonitoringSystem.Services.Customer.Implementations.Watchlist
{
    using global::FraudMonitoringSystem.DTOs.Watchlist;
    using global::FraudMonitoringSystem.Exceptions.Watchlist.FraudMonitoringSystem.Exceptions;
    using global::FraudMonitoringSystem.Models.WatchList;
    using global::FraudMonitoringSystem.Repositories.Customer.Interfaces.Watchlist;
    using global::FraudMonitoringSystem.Services.Customer.Interfaces.Watchlist;

    namespace FraudMonitoringSystem.Services.Implementations
    {
        public class EntityLinkService : IEntityLinkService
        {
            private readonly IEntityLinkRepository _repo;

            public EntityLinkService(IEntityLinkRepository repo)
            {
                _repo = repo;
            }

            public async Task<IEnumerable<EntityLinkDto>> GetLinksByCustomerIdAsync(long customerId)
            {
                var links = await _repo.GetLinksByCustomerIdAsync(customerId);
                if (!links.Any())
                    throw new NotFoundException($"No links found for Customer {customerId}");

                return links.Select(l => new EntityLinkDto
                {
                    CustomerId = l.FromCustomerId,
                    AccountId = l.ToAccountId,
                    LinkType = l.LinkType
                });
            }

            public async Task<IEnumerable<EntityLinkDto>> GetLinksByAccountIdAsync(long accountId)
            {
                var links = await _repo.GetLinksByAccountIdAsync(accountId);
                if (!links.Any())
                    throw new NotFoundException($"No links found for Account {accountId}");

                return links.Select(l => new EntityLinkDto
                {
                    CustomerId = l.FromCustomerId,
                    AccountId = l.ToAccountId,
                    LinkType = l.LinkType
                });
            }

            public async Task AddLinkAsync(EntityLinkDto dto)
            {
                if (dto.CustomerId <= 0 || dto.AccountId <= 0)
                    throw new ValidationException("CustomerId and AccountId must be valid");

                var link = new EntityLink
                {
                    FromCustomerId = dto.CustomerId,
                    ToAccountId = dto.AccountId,
                    LinkType = dto.LinkType
                };

                var result = await _repo.AddLinkAsync(link);
                if (result == 0)
                    throw new BusinessRuleException("Failed to add entity link");
            }

            public async Task DeleteLinkAsync(long linkId)
            {
                var result = await _repo.DeleteLinkAsync(linkId);
                if (result == 0)
                    throw new NotFoundException($"Entity link {linkId} not found");
            }
        }
    }
}

