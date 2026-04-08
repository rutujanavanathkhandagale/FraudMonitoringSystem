using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations.Admin
{
    public class RoleRepository : IRoleRepository
    {
        private readonly WebContext _context;


        public RoleRepository(WebContext context)

        {
            _context = context;

        }

        public async Task<List<Role>> GetAllAsync() =>
            await _context.Roles.OrderBy(r => r.RoleName).ToListAsync();


        public async Task<Role?> GetByIdAsync(string id) =>
            await _context.Roles.FindAsync(id);


        public async Task<Role?> GetByNameAsync(string name) =>
            await _context.Roles

                .FirstOrDefaultAsync(r => r.RoleName.ToLower() == name.ToLower());


        public async Task AddAsync(Role role) =>
            await _context.Roles.AddAsync(role);


        public async Task DeleteAsync(Role role) =>
            _context.Roles.Remove(role);


        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();


        // ✅ R101 logic
        public async Task<string?> GetLastRoleIdAsync()

        {

            return await _context.Roles

                .OrderByDescending(r => r.RoleId)

                .Select(r => r.RoleId)

                .FirstOrDefaultAsync();

        }

    }

}

 