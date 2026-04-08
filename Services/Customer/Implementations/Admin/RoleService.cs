using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Exceptions;
using FraudMonitoringSystem.Exceptions.Admin;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using System.Text.RegularExpressions;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Admin
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;


        public RoleService(IRoleRepository repo)

        {
            _repo = repo;

        }

        public async Task<Role> CreateAsync(RoleCreateDto dto)

        {
            if (string.IsNullOrWhiteSpace(dto.RoleName))

                throw new InvalidRoleException("Role name is required");


            var name = dto.RoleName.Trim();

            if (!Regex.IsMatch(name, @"^[A-Za-z ]+$"))

                throw new InvalidRoleException("Role name must contain letters only");


            var exists = await _repo.GetByNameAsync(name);

            if (exists != null)

                throw new RoleAlreadyyExistsException("Role already exists");


            // ✅ R101 generation
            string nextId;

            var lastId = await _repo.GetLastRoleIdAsync();


            if (string.IsNullOrEmpty(lastId))

                nextId = "R101";

            else
            {
                var num = int.Parse(lastId.Substring(1));

                nextId = $"R{num + 1}";

            }

            var role = new Role
            {
                RoleId = nextId,

                RoleName = name,

                Description = dto.Description?.Trim()

            };

            await _repo.AddAsync(role);

            await _repo.SaveAsync();

            return role;

        }

        public async Task<List<Role>> GetAllAsync() =>
            await _repo.GetAllAsync();


        public async Task<Role> GetByIdAsync(string id)

        {
            if (string.IsNullOrWhiteSpace(id))

                throw new InvalidRoleException("Invalid Role ID");


            return await _repo.GetByIdAsync(id)

                ?? throw new RoleNotFoundException("Role not found");

        }

        public async Task<Role> UpdateAsync(string id, RoleUpdateDto dto)

        {
            var role = await GetByIdAsync(id);


            var name = dto.RoleName?.Trim();

            if (string.IsNullOrWhiteSpace(name))

                throw new InvalidRoleException("Role name is required");


            var exists = await _repo.GetByNameAsync(name);

            if (exists != null && exists.RoleId != id)

                throw new RoleAlreadyyExistsException("Role already exists");


            role.RoleName = name;

            role.Description = dto.Description?.Trim();


            await _repo.SaveAsync();

            return role;

        }


        public async Task DeleteAsync(string id)

        {

            var role = await GetByIdAsync(id);

            await _repo.DeleteAsync(role);

            await _repo.SaveAsync();

        }

    }

}