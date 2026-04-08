using FraudMonitoringSystem.Authentication;
using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Exceptions.Admin;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Admin
{
    public class SystemUserService : ISystemUserService
    {
        private readonly ISystemUserRepository _repository;

        private readonly IRegistrationRepository _registrationRepository;


        private readonly WebContext _context;


        public SystemUserService(

            ISystemUserRepository repository,

            IRegistrationRepository registrationRepository,

            WebContext context)

        {
            _repository = repository;

            _registrationRepository = registrationRepository;

            _context = context;

        }

        public async Task<List<SystemUserResponseDto>> GetAllAsync(int page, int pageSize)

        {
            var users = await _repository.GetAllAsync(page, pageSize);


            return users.Select(x => new SystemUserResponseDto
            {
                SystemUserId = x.Id,

                SystemUserCode = x.SystemUserCode,

                IsApproved = x.IsApproved,

                ApprovedAt = x.ApprovedAt,

                ApprovedBy = x.ApprovedBy,


                RegistrationId = x.Registration.RegistrationId,

                FirstName = x.Registration.FirstName,

                LastName = x.Registration.LastName,

                Email = x.Registration.Email,

                PhoneNo = x.Registration.PhoneNo,


                Role = x.Role?.RoleName
            }).ToList();

        }

        public async Task<List<SystemUserResponseDto>> GetByRoleIdAsync(string roleId)

        {
            if (string.IsNullOrWhiteSpace(roleId))

                throw new InvalidRoleException("Invalid role id");


            var users = await _repository.GetByRoleIdAsync(roleId);


            if (!users.Any())

                throw new RoleNotFoundException("No system users found for this role");


            return users.Select(x => new SystemUserResponseDto
            {
                SystemUserId = x.Id,

                SystemUserCode = x.SystemUserCode,

                IsApproved = x.IsApproved,

                ApprovedAt = x.ApprovedAt,

                ApprovedBy = x.ApprovedBy,


                RegistrationId = x.Registration.RegistrationId,

                FirstName = x.Registration.FirstName,

                LastName = x.Registration.LastName,

                Email = x.Registration.Email,

                PhoneNo = x.Registration.PhoneNo,


                Role = x.Role?.RoleName

            }).ToList();

        }

        public async Task AddAsync(SystemUserCreateDto dto)

        {
            var registration = await _registrationRepository.GetByIdAsync(dto.RegistrationId);

            if (registration == null)

                throw new RoleNotFoundException("Registration not found");


            if (await _repository.ExistsByRegistrationId(dto.RegistrationId))

                throw new RoleAlreadyyExistsException("System user already exists for this registration");


            var nextNumber = await _repository.GetNextSystemUserNumberAsync();


            var user = new SystemUser
            {
                RegistrationId = dto.RegistrationId,

                RoleId = dto.RoleId,

                SystemUserCode = $"B{nextNumber}",

                IsApproved = false
            };

            await _repository.AddAsync(user);


            _context.AuditLogs.Add(new AuditLog
            {
                EntityType = "SystemUser",

                EntityId = user.Id,

                Action = "CREATE",

                Description = $"System user {user.SystemUserCode} created",

                PerformedBy = dto.RegistrationId

            });

            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)

        {
            var user = await _repository.GetByIdAsync(id)

                ?? throw new RoleNotFoundException("System user not found");


            await _repository.DeleteAsync(user);


            _context.AuditLogs.Add(new AuditLog
            {
                EntityType = "SystemUser",

                EntityId = user.Id,

                Action = "DELETE",

                Description = $"System user {user.SystemUserCode} deleted",

                PerformedBy = user.ApprovedBy ?? 0

            });


            await _context.SaveChangesAsync();

        }




        public async Task ApproveAsync(int systemUserId, int adminRegistrationId)

        {

            var user = await _repository.GetByIdAsync(systemUserId)

                ?? throw new RoleNotFoundException("System user not found");


            if (user.IsApproved)

                throw new InvalidRoleException("System user already approved");


            await _repository.ApproveAsync(user, adminRegistrationId);



            _context.AuditLogs.Add(new AuditLog

            {

                EntityType = "SystemUser",

                EntityId = user.Id,

                Action = "APPROVE",

                Description = $"System user {user.SystemUserCode} approved",

                PerformedBy = adminRegistrationId

            });


            await _context.SaveChangesAsync();

        }

        public async Task<SystemUserResponseDto> GetByIdAsync(int id)

        {

            var user = await _repository.GetByIdAsync(id)

                ?? throw new Exception("System user not found");


            return new SystemUserResponseDto

            {

                SystemUserId = user.Id,

                SystemUserCode = user.SystemUserCode,

                IsApproved = user.IsApproved,

                ApprovedAt = user.ApprovedAt,

                ApprovedBy = user.ApprovedBy,


                RegistrationId = user.Registration.RegistrationId,

                FirstName = user.Registration.FirstName,

                LastName = user.Registration.LastName,

                Email = user.Registration.Email,

                PhoneNo = user.Registration.PhoneNo,


                Role = user.Role?.RoleName

            };

        }

    }

}