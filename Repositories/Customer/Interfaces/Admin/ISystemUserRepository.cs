using FraudMonitoringSystem.Models.Admin;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin
{
    public interface ISystemUserRepository
    {
        Task<List<SystemUser>> GetAllAsync(int page, int pageSize);
        Task<SystemUser?> GetByIdAsync(int id);
        Task<List<SystemUser>> GetByRoleIdAsync(string roleId);

        // ✅ ONLY ONE COUNT METHOD (SAFE)
        Task<int> CountActiveByRoleIdAsync(string roleId);

        Task AddAsync(SystemUser user);
        Task DeleteAsync(SystemUser user);

        Task<bool> ExistsByRegistrationId(int registrationId);
        Task<int> GetNextSystemUserNumberAsync();
    }
}
