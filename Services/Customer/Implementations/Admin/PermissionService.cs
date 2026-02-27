using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Exceptions.Admin;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Admin
{
    public class PermissionService : IPermissionService

    {

        private readonly IPermissionRepository _repository;

        public PermissionService(IPermissionRepository repository)

        {

            _repository = repository;

        }

        public async Task<List<PermissionResponseDto>> GetAllAsync()

        {

            var permissions = await _repository.GetAllAsync();

            return permissions.Select(p => new PermissionResponseDto

            {

                PermissionId = p.PermissionId,

                ModuleName = p.ModuleName,

                ActionName = p.ActionName,

                Description = p.Description

            }).ToList();

        }

        public async Task<PermissionResponseDto> GetByIdAsync(int id)

        {

            var permission = await _repository.GetByIdAsync(id);

            if (permission == null)

                throw new PermissionNotFoundException(id);

            return new PermissionResponseDto

            {

                PermissionId = permission.PermissionId,

                ModuleName = permission.ModuleName,

                ActionName = permission.ActionName,

                Description = permission.Description

            };

        }

        public async Task<string> CreateAsync(PermissionCreateDto dto)

        {

            var existing = await _repository

                .GetByModuleAndActionAsync(dto.ModuleName, dto.ActionName);

            if (existing != null)

                throw new PermissionAlreadyExistsException(dto.ModuleName, dto.ActionName);

            var permission = new Permission

            {

                ModuleName = dto.ModuleName,

                ActionName = dto.ActionName,

                Description = dto.Description

            };

            await _repository.AddAsync(permission);

            await _repository.SaveAsync();

            return "Permission created successfully.";

        }

        public async Task<string> UpdateAsync(int id, PermissionUpdateDto dto)
        {
            if (id != dto.PermissionId)
                throw new ArgumentException("Route Id and Body Id do not match");
            var permission = await _repository.GetByIdAsync(id);
            if (permission == null)
                throw new PermissionNotFoundException(id);
            permission.ModuleName = dto.ModuleName;
            permission.ActionName = dto.ActionName;
            permission.Description = dto.Description;
            await _repository.UpdateAsync(permission);
            await _repository.SaveAsync();
            return "Permission updated successfully.";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var permission = await _repository.GetByIdAsync(id);
            if (permission == null)
                throw new PermissionNotFoundException(id);
            await _repository.DeleteAsync(permission);
            await _repository.SaveAsync();
            return "Permission deleted successfully.";
        }
    }

}