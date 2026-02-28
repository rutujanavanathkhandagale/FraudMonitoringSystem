using FraudMonitoringSystem.Controllers.Admin;
using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Interfaces;
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
        //public async Task<List<Registration>> GetSystemUsersAsync()

        //{

        //    return await _context.Registrations

        //        .Where(r => r.Role != RegisterRole.Customer)

        //        .ToListAsync();

        //}

        public async Task AddSystemUserAsync(Registration registration)
        {
            var systemUser = new SystemUser
            {
                RegistrationId = registration.RegistrationId,
                Role = (AdminRole)registration.Role
            };
            await _context.SystemUsers.AddAsync(systemUser);
            await _context.SaveChangesAsync();
        }
        public async Task<Registration?> GetByIdAsync(int id)
        {
            return await _context.Registrations
                .FirstOrDefaultAsync(r => r.RegistrationId == id);
        }

    }
}
