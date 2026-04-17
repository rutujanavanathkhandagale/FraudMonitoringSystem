using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace FraudMonitoringSystem.Repositories.Customer.Implementations
{
    public class KYCRepository : IKYCRepository
    {
        private readonly WebContext _context;
        public KYCRepository(WebContext context)
        {
            _context = context;
        }
        public async Task<KYCProfile?> GetByIdAsync(long id)
        {
            return await _context.KYCProfile
                .Include(k => k.Customer)
                .FirstOrDefaultAsync(k => k.KYCId == id);
        }
        public async Task<int> GetPendingKycCountAsync()
        {
            return await _context.KYCProfile.CountAsync(k => k.Status == "Pending");
        }
        public async Task<int> GetVerifiedKycCountAsync()
        {
            return await _context.KYCProfile.CountAsync(k => k.Status == "Verified");
        }

        public async Task<KYCProfile?> GetByCustomerIdAsync(long customerId)
        {
            return await _context.KYCProfile
                .Include(k => k.Customer)
                .FirstOrDefaultAsync(k => k.CustomerId == customerId);
        }
        public async Task<KYCProfile?> VerifyByCustomerIdAsync(long customerId)
        {
            var profile = await _context.KYCProfile
                                        .FirstOrDefaultAsync(p => p.CustomerId == customerId);

            if (profile == null) return null;

            profile.Status = "Verified";
            _context.KYCProfile.Update(profile);
            await _context.SaveChangesAsync();

            return profile;
        }


        public async Task<List<KYCProfile>> SearchAsync(string query)
        {
            return await _context.KYCProfile
                .Include(k => k.Customer)
                .Where(k => k.Customer.CustomerType.Contains(query))
                .ToListAsync();
        }
        public async Task<KYCProfile> AddAsync(KYCProfile profile)
        {
            _context.KYCProfile.Add(profile);
            await _context.SaveChangesAsync();
            return profile;
        }
        public async Task<KYCProfile?> VerifyAsync(KYCProfile profile)
        {
            profile.Status = "Verified";
            _context.KYCProfile.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }
        public async Task<IEnumerable<KYCProfile>> GetAllWithDetailsAsync()
        {
            // Accessing the DbContext directly inside the Repository
            return await _context.KYCProfile
                .Include(k => k.Customer)
                .ToListAsync();
        }
    }
}
