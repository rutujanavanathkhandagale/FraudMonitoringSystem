using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Exceptions.Admin;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Repositories.Interfaces;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Admin
{
    public class SystemUserService : ISystemUserService
    {
        private readonly ISystemUserRepository _repository;
        private readonly IRegistrationRepository _registrationRepository;
        public SystemUserService(
            ISystemUserRepository repository,
            IRegistrationRepository registrationRepository)
        {
            _repository = repository;
            _registrationRepository = registrationRepository;
        }
        public async Task<List<SystemUserResponseDto>> GetAllAsync(int page, int pageSize)
        {
            var users = await _repository.GetAllAsync(page, pageSize);
            return users.Select(x => new SystemUserResponseDto
            {
                Id = x.Id,
                //SystemUserCode = x.SystemUserCode,
                FirstName = x.Registration!.FirstName,
                LastName = x.Registration!.LastName,
                Role = x.Role.ToString()
            }).ToList();
        }
        public async Task<List<SystemUserResponseDto>> GetByRoleAsync(AdminRole role)
        {
            var users = await _repository.GetByRoleAsync(role);
            return users.Select(x => new SystemUserResponseDto
            {
                Id = x.Id,
                FirstName = x.Registration!.FirstName,
                LastName = x.Registration!.LastName,
                //SystemUserCode = x.SystemUserCode,
                //Name = x.Registration!.Name,
                Role = x.Role.ToString()
            }).ToList();
        }
        public async Task AddAsync(SystemUserCreateDto dto)
        {
            var registration = await _registrationRepository.GetByIdAsync(dto.RegistrationId);
            if (registration == null)
                throw new NotFoundException("Registration not found");
            if (registration.Role == RegisterRole.Customer)
                throw new BusinessException("Customer cannot be system user");
            if (await _repository.ExistsByRegistrationId(dto.RegistrationId))
                throw new DuplicateException("System user already exists");
            var count = await _repository.CountByRoleAsync(dto.Role);
            var prefix = dto.Role.ToString().Substring(0, 3).ToUpper();
            var generatedCode = $"{prefix}-{(count + 1):D3}";
            var user = new SystemUser
            {
                RegistrationId = dto.RegistrationId,
                Role = dto.Role
            };
            await _repository.AddAsync(user);
        }
        public async Task DeleteAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException("SystemUser not found");
            await _repository.DeleteAsync(user);
        }
    }
}