using FraudMonitoringSystem.Models.Admin;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin
{
    public interface IRoleRepository
    {

        Task<List<Role>> GetAllAsync();

        Task<Role?> GetByIdAsync(string id);

        Task<Role?> GetByNameAsync(string name);

        Task AddAsync(Role role);

        Task DeleteAsync(Role role);

        Task SaveAsync();

        Task<string?> GetLastRoleIdAsync(); // NEW


    }
}