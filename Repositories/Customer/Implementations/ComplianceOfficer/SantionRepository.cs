using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.ComplianceOfficer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace FraudMonitoringSystem.Repositories.Customer.Implementations
{
    public class SanctionRepository : ISanctionRepository
    {
        private readonly WebContext _context;
        public SanctionRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<Sanction> AddAsync(Sanction model)
        {
            _context.Sanctions.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
     
        public async Task<object> CheckSanctionAsync(long customerId)
        {
            var customer = await _context.PersonalDetails
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (customer == null)
            {
                return new
                {
                    Found = false,
                    Message = "Customer not found"
                };
            }
            var match = await _context.Sanctions
                .FirstOrDefaultAsync(s =>
                    s.FirstName == customer.FirstName &&
                    s.DOB == customer.DOB &&
                    s.PermanentAddress == customer.PermanentAddress);
            if (match == null)
            {
                return new
                {
                    Found = false,
                    Message = "No sanction match found"
                };
            }
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.CustomerId == customer.CustomerId);
            if (account != null)
            {
                account.Status = "Inactive";
                await _context.SaveChangesAsync();
            }
            return new
            {
                Found = true,
                Message = "Sanction match found. Account set to Inactive."
            };
        }
    }
}