using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.ComplianceOfficer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace FraudMonitoringSystem.Repositories.Customer.Implementations
{
    public class PEPListRepository : IPEPListRepository
    {
        private readonly WebContext _context;
        public PEPListRepository(WebContext context)
        {
            _context = context;
        }
        // 🔹 POST - Add PEP
        public async Task<PEPListModel> AddAsync(PEPListModel model)
        {
            _context.PEPList.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
        // 🔹 GET - Check PEP Screening
        public async Task<object> CheckPepAsync(long customerId)
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
            var pepMatch = await _context.PEPList
                .FirstOrDefaultAsync(p =>
                    p.FirstName == customer.FirstName &&
                    p.DOB == customer.DOB &&
                    p.PermanentAddress == customer.PermanentAddress);
            if (pepMatch == null)
            {
                return new
                {
                    Found = false,
                    Message = "No PEP match found"
                };
            }
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.CustomerId == customerId);
            if (account != null)
            {
                account.Status = "Inactive";
                await _context.SaveChangesAsync();
            }
            return new
            {
                Found = true,
                Message = "PEP match found. Account set to Inactive."
            };
        }
    }
}