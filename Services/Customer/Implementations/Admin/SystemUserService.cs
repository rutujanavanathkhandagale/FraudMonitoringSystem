using FraudMonitoringSystem.Authentication;
using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Exceptions.Admin;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Services.Customer.Implementations.Common;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Admin
{
    public class SystemUserService : ISystemUserService
    {
        private readonly ISystemUserRepository _repo;
        private readonly IRegistrationRepository _regRepo;
        private readonly WebContext _context;
        private readonly IAuditLogService _audit;

        public SystemUserService(
            ISystemUserRepository repo,
            IRegistrationRepository regRepo,
            WebContext context,
            IAuditLogService audit)
        {
            _repo = repo;
            _regRepo = regRepo;
            _context = context;
            _audit = audit;
        }

        public async Task<List<SystemUserResponseDto>> GetAllAsync(int page, int pageSize)
        {
            var users = await _repo.GetAllAsync(page, pageSize);

            return users.Select(x => MapToDto(x)).ToList();
        }

        public async Task<SystemUserResponseDto> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id)
                ?? throw new Exception("System user not found");

            return MapToDto(user);
        }

        public async Task<List<SystemUserResponseDto>> GetByRoleIdAsync(string roleId)
        {
            var users = await _repo.GetByRoleIdAsync(roleId);
            return users.Select(x => MapToDto(x)).ToList();
        }

        public async Task AddAsync(SystemUserCreateDto dto)
        {
            if (await _repo.ExistsByRegistrationId(dto.RegistrationId))
                throw new Exception("System user already exists");

            var next = await _repo.GetNextSystemUserNumberAsync();

            var user = new SystemUser
            {
                RegistrationId = dto.RegistrationId,
                RoleId = dto.RoleId,
                SystemUserCode = $"B{next}",
                IsApproved = false,
                IsActive = false
            };

            await _repo.AddAsync(user);

            await _audit.LogAsync(
                "SystemUser",
                user.Id.ToString(),
                "CREATE",
                $"System user {user.SystemUserCode} created",
                dto.RegistrationId
            );
        }

        public async Task ApproveAsync(int id, int adminId)
        {
            var user = await _repo.GetByIdAsync(id)
                ?? throw new Exception("System user not found");

            if(user.IsApproved)
             return;


            user.IsApproved = true;
            user.IsActive = true;
            user.ApprovedAt = DateTime.UtcNow;
            user.ApprovedBy = adminId;

            await _context.SaveChangesAsync();

            await _audit.LogAsync(
                "SystemUser",
                user.Id.ToString(),
                "APPROVE",
                $"System user {user.SystemUserCode} approved",
                adminId
            );
        }

        public async Task DeactivateAsync(int id, int adminId)
        {
            var user = await _repo.GetByIdAsync(id)
                ?? throw new Exception("System user not found");

            if (user.Role != null)
            {
                user.LastAssignedRoleId = user.RoleId;
                user.LastAssignedRoleName = user.Role.RoleName;
                user.RoleId = null;
            }

            user.IsActive = false;

            await _context.SaveChangesAsync();

            await _audit.LogAsync(
                "SystemUser",
                user.Id.ToString(),
                "DEACTIVATE",
                $"System user {user.SystemUserCode} deactivated",
                adminId
            );
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id)
                ?? throw new Exception("System user not found");

            await _repo.DeleteAsync(user);
        }

        private static SystemUserResponseDto MapToDto(SystemUser x) =>
            new SystemUserResponseDto
            {
                SystemUserId = x.Id,
                SystemUserCode = x.SystemUserCode,
                IsApproved = x.IsApproved,
                IsActive = x.IsActive,
                ApprovedAt = x.ApprovedAt,
                ApprovedBy = x.ApprovedBy,
                RegistrationId = x.RegistrationId,
                FirstName = x.Registration.FirstName,
                LastName = x.Registration.LastName,
                Email = x.Registration.Email,
                PhoneNo = x.Registration.PhoneNo,
                Role = x.Role?.RoleName,
                LastAssignedRoleId = x.LastAssignedRoleId,
                LastAssignedRoleName = x.LastAssignedRoleName
            };

        public async Task ChangeRoleAsync(
    int systemUserId,
    string newRoleId,
    int adminRegistrationId)
        {
            var user = await _repo.GetByIdAsync(systemUserId)
                ?? throw new Exception("System user not found");

            // ✅ Only ACTIVE & APPROVED users
            if (!user.IsApproved || !user.IsActive)
                throw new Exception("Role can be changed only for active users");

            // ✅ Store previous role (IMPORTANT)
            user.LastAssignedRoleId = user.RoleId;
            user.LastAssignedRoleName = user.Role?.RoleName;

            // ✅ Assign new role
            user.RoleId = newRoleId;

            await _context.SaveChangesAsync();

            // ✅ AUDIT LOG
            await _audit.LogAsync(
                "SystemUser",
                user.Id.ToString(),
                "ROLE_CHANGE",
                $"Role changed from {user.LastAssignedRoleName} to {user.Role?.RoleName}",
                adminRegistrationId
            );
        }
    }
    }