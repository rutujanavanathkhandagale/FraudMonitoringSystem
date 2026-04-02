using FraudMonitoringSystem.Authentication;
using FraudMonitoringSystem.DTOs;
using FraudMonitoringSystem.Exceptions;
using FraudMonitoringSystem.Exceptions.Customer;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Services.Interfaces;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Admin
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _repository;

        public RegistrationService(IRegistrationRepository repository)
        {
            _repository = repository;
        }

        public async Task<RegistrationDto> RegisterAsync(RegistrationDto dto)
        {
            // 1. Check if user already exists based on Email [cite: 232, 233]
            var existingUser = await _repository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new RegisterUserAlreadyExistsException("Email already exists");

            // 2. Validation for matching passwords
            if (dto.Password != dto.ConfirmPassword)
                throw new RegisterValidationException("Passwords do not match");

            // 3. Robust Enum Parsing to handle spaces from Frontend (e.g., "Compliance Officer" -> "ComplianceOfficer")
            // This prevents the 'Requested value not found' exception
            string cleanedRole = dto.Role.Replace(" ", "");
            if (!Enum.TryParse<RegisterRole>(cleanedRole, true, out var parsedRole))
            {
                throw new RegisterValidationException($"Invalid Role provided: {dto.Role}");
            }

            var registration = new Registration
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNo = dto.PhoneNo,
                // Hash password for security in Identity & Access Management [cite: 15, 40]
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                ConfirmPassword = dto.ConfirmPassword,
                Role = parsedRole,

                UserInfo = new UserInfo
                {
                    IsEmailVerified = false,
                    VerificationOtp = null,
                    OtpExpiry = null,
                    ResetToken = null,
                    ResetTokenExpiry = null
                }
            };

            // 4. Save to Database [cite: 228, 230]
            var result = await _repository.RegisterAsync(registration);
            if (result == 0)
                throw new RegisterDatabaseException("Registration failed to save in database");

            // 5. Logic for System Users (Analysts, Investigators, Compliance Officers, etc.) [cite: 8, 9, 10, 39]
            if (registration.Role != RegisterRole.Customer)
            {
                await _repository.AddSystemUserAsync(registration);
            }

            return new RegistrationDto
            {
                RegistrationId = registration.RegistrationId,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Email = registration.Email,
                PhoneNo = registration.PhoneNo,
                Role = registration.Role.ToString()
            };
        }

        public async Task<RegistrationDto> GetUserByRoleAsync(RegisterRole role)
        {
            var user = await _repository.GetByRoleAsync(role);
            if (user == null)
                throw new RegisterUserNotFoundException($"No user found with role {role}");

            return new RegistrationDto
            {
                RegistrationId = user.RegistrationId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNo = user.PhoneNo,
                Role = user.Role.ToString()
            };
        }
    }
}