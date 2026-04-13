using FraudMonitoringSystem.Controllers.Admin;
using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Models.Customer;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Authentication
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
                .AsNoTracking() // optional, since we’re just reading
                .FirstOrDefaultAsync(r => r.Email == email);
        }
        public async Task<Registration?> GetByIdAsync(int id)
        {
            return await _context.Registrations
                .FirstOrDefaultAsync(r => r.RegistrationId == id);
        }

        public async Task<Registration?> GetByRoleAsync(RegisterRole role)
        {
            return await _context.Registrations

             .FirstOrDefaultAsync(r => r.Role == role);
        }







        //Admin Part

        public async Task AddSystemUserAsync(Registration registration)
        {
            var exists = await _context.SystemUsers
                .AnyAsync(u => u.RegistrationId == registration.RegistrationId);

            if (exists)
            {
                return;
            }

            // ✅ Map RoleName → RoleId
            var roleName = registration.Role.ToString();
            var role = await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RoleName == roleName);

            if (role == null)
            {
                throw new InvalidOperationException(
                    $"Role '{roleName}' not found in Roles table. Seed/create the role first.");
            }

            // 1. Fetch codes from DB only (SQL‑safe)
            var codes = await _context.SystemUsers
                .Select(u => u.SystemUserCode)
                .Where(code => code.StartsWith("B"))
                .ToListAsync();

            // 2. Parse in memory (C#)
            var nextNumber = codes
                .Select(code => int.Parse(code.Substring(1)))
                .DefaultIfEmpty(299)
                .Max() + 1;


            var systemUserCode = $"B{nextNumber}";

            // ✅ Create SystemUser with REQUIRED fields
            var systemUser = new SystemUser
            {
                RegistrationId = registration.RegistrationId,
                RoleId = role.RoleId,
                SystemUserCode = systemUserCode,
                IsApproved = false
            };

            await _context.SystemUsers.AddAsync(systemUser);
            await _context.SaveChangesAsync();
        }

    }
}
