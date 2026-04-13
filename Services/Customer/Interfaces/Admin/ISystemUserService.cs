using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Models.Customer;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.Admin
{
    public interface ISystemUserService
    {
        Task<List<SystemUserResponseDto>> GetAllAsync(int page, int pageSize);
        Task<List<SystemUserResponseDto>> GetByRoleIdAsync(string roleId);
        Task<SystemUserResponseDto> GetByIdAsync(int id);

        Task AddAsync(SystemUserCreateDto dto);
        Task ApproveAsync(int systemUserId, int adminRegistrationId);
        Task DeactivateAsync(int systemUserId, int adminRegistrationId);
        Task DeleteAsync(int id);
        Task ChangeRoleAsync(
    int systemUserId,
    string newRoleId,
    int adminRegistrationId
);

    }
}