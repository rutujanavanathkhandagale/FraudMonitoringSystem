using FraudMonitoringSystem.DTOs.Watchlist;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.Watchlist
{
   
  


    
        public interface IEntityLinkService
        {
            Task<IEnumerable<EntityLinkDto>> GetLinksByCustomerIdAsync(long customerId);
            Task<IEnumerable<EntityLinkDto>> GetLinksByAccountIdAsync(long accountId);
            Task AddLinkAsync(EntityLinkDto dto);
            Task DeleteLinkAsync(long linkId);
        }
    }


