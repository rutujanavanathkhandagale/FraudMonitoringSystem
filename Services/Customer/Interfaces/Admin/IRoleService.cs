using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Models.Admin;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.Admin
{
    public interface IRoleService
    {


        Task<Role> CreateAsync(RoleCreateDto dto);

        Task<List<Role>> GetAllAsync();

        Task<Role> GetByIdAsync(string id);

        Task<Role> UpdateAsync(string id, RoleUpdateDto dto);

        Task DeleteAsync(string id);


    }

}
