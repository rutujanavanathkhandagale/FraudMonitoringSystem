using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Exceptions.Admin;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Admin
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly WebContext _context;
        private readonly IAuditLogService _auditLogService;

        public RoleService(
            IRoleRepository roleRepository,
            ISystemUserRepository systemUserRepository,
            WebContext context,
            IAuditLogService auditLogService)
        {
            _roleRepository = roleRepository;
            _systemUserRepository = systemUserRepository;
            _context = context;
            _auditLogService = auditLogService;
        }

        // ✅ CREATE
        public async Task<Role> CreateAsync(RoleCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RoleName))
                throw new InvalidRoleException("Role name is required");

            var name = dto.RoleName.Trim();

            if (!Regex.IsMatch(name, @"^[A-Za-z ]+$"))
                throw new InvalidRoleException("Role name must contain letters only");

            if (await _roleRepository.GetByNameAsync(name) != null)
                throw new RoleAlreadyyExistsException("Role already exists");

            var lastId = await _roleRepository.GetLastRoleIdAsync();
            var nextId = string.IsNullOrEmpty(lastId)
                ? "R101"
                : $"R{int.Parse(lastId.Substring(1)) + 1}";

            var role = new Role
            {
                RoleId = nextId,
                RoleName = name,
                Description = dto.Description?.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveAsync();

            await _auditLogService.LogAsync(
                "Role",
                role.RoleId,
                "CREATE",
                $"Role '{role.RoleName}' created",
                1
            );

            return role;
        }

        // ✅ GET ALL WITH USER COUNT
        public async Task<List<RoleResponseDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            var result = new List<RoleResponseDto>();

            foreach (var role in roles)
            {
                var count = await _systemUserRepository.CountActiveByRoleIdAsync(role.RoleId);
                result.Add(new RoleResponseDto
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    ActiveUserCount = count,
                    CreatedAt = role.CreatedAt
                });
            }

            return result;
        }

        public async Task<Role> GetByIdAsync(string id)
        {
            return await _roleRepository.GetByIdAsync(id)
                ?? throw new RoleNotFoundException("Role not found");
        }

        public async Task<Role> UpdateAsync(string id, RoleUpdateDto dto)
        {
            var role = await GetByIdAsync(id);

            role.RoleName = dto.RoleName?.Trim()
                ?? throw new InvalidRoleException("Role name required");

            role.Description = dto.Description?.Trim();

            await _roleRepository.SaveAsync();

            await _auditLogService.LogAsync(
                "Role",
                role.RoleId,
                "UPDATE",
                $"Role '{role.RoleName}' updated",
                1
            );

            return role;
        }

        // ✅ DELETE + DEACTIVATE USERS
        public async Task DeleteAsync(string roleId)
        {
            var role = await GetByIdAsync(roleId);

            var users = await _context.SystemUsers
                .Where(u => u.RoleId == roleId)
                .ToListAsync();

            foreach (var user in users)
            {
                user.LastAssignedRoleId = role.RoleId;
                user.LastAssignedRoleName = role.RoleName;
                user.RoleId = null;
                user.IsActive = false;
            }

            await _context.SaveChangesAsync();

            await _roleRepository.DeleteAsync(role);
            await _roleRepository.SaveAsync();

            await _auditLogService.LogAsync(
                "Role",
                role.RoleId,
                "DELETE",
                $"Role '{role.RoleName}' deleted",
                1
            );
        }
    }
}
