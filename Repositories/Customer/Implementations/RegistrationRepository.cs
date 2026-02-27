using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Interfaces;
using FraudMonitoringSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Repositories.Implementations
{
   
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly WebContext _context;

        public RegistrationRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<int> RegisterAsync(Registration registration)
        {
            await _context.Registrations.AddAsync(registration);
            return await _context.SaveChangesAsync();
        }

        public async Task<Registration?> GetByEmailAsync(string email)
        {
            return await _context.Registrations
                                 .FirstOrDefaultAsync(r => r.Email == email);
        }

        public async Task<Registration?> GetByRoleAsync(RegisterRole role)
        {
            return await _context.Registrations
                                 .FirstOrDefaultAsync(r => r.Role == role);
        }
    }
}
