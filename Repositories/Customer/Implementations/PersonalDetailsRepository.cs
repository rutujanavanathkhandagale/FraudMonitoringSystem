using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations
{
    public class PersonalDetailsRepository : IPersonalDetailsRepository
    {
        private readonly WebContext _context;

        public PersonalDetailsRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(PersonalDetails details)
        {
            await _context.PersonalDetails.AddAsync(details);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> GetCustomerCountAsync()
        {
            return await _context.PersonalDetails.CountAsync();
        }

        public async Task<PersonalDetails?> GetByIdAsync(long id)
        {
            return await _context.PersonalDetails.FindAsync(id);
        }

        public async Task<List<PersonalDetails>> GetAllAsync()
        {
            return await _context.PersonalDetails.ToListAsync();
        }

        public async Task<List<PersonalDetails>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<PersonalDetails>();

            return await _context.PersonalDetails
                .Where(p =>
                    (!string.IsNullOrEmpty(p.FirstName) && p.FirstName.ToLower() == name.ToLower()) ||
                    (!string.IsNullOrEmpty(p.MiddleName) && p.MiddleName.ToLower() == name.ToLower()) ||
                    (!string.IsNullOrEmpty(p.LastName) && p.LastName.ToLower() == name.ToLower())
                )
                .ToListAsync();
        }

        public async Task<PersonalDetails?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return await _context.PersonalDetails
                .FirstOrDefaultAsync(p => p.Email.ToLower() == email.ToLower());
        }


        public async Task<int> UpdateAsync(PersonalDetails details)
        {
            var existing = await _context.PersonalDetails.FindAsync(details.CustomerId);
            if (existing == null) return 0;

            _context.Entry(existing).CurrentValues.SetValues(details);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(long id)
        {
            var existing = await _context.PersonalDetails.FindAsync(id);
            if (existing == null) return 0;

            _context.PersonalDetails.Remove(existing);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> PatchAsync(PersonalDetails details)
        {
            var existing = await _context.PersonalDetails.FindAsync(details.CustomerId);
            if (existing == null) return 0;

            _context.Entry(existing).CurrentValues.SetValues(details);
            return await _context.SaveChangesAsync();
        }
    }
}
