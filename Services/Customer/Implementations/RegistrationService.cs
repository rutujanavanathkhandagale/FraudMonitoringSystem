using FraudMonitoringSystem.Exceptions;
using FraudMonitoringSystem.Exceptions.Customer;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Interfaces;
using FraudMonitoringSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Admin
{
    
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _repository;

        public RegistrationService(IRegistrationRepository repository)
        {
            _repository = repository;
        }

       
    
   
        public async Task<string> RegisterAsync(Registration registration)
        {
        
            var existingUser = await _repository.GetByEmailAsync(registration.Email);
            if (existingUser != null)
                throw new RegisterUserAlreadyExistsException("Email already exists");

         
            if (registration.Password != registration.ConfirmPassword)
                throw new RegisterValidationException("Passwords do not match");

            var result = await _repository.RegisterAsync(registration);
            if (result == 0)
                throw new RegisterDatabaseException("Registration failed to save in database");

          
            if (registration.Role != RegisterRole.Customer)
            {
                await _repository.AddSystemUserAsync(registration);
            }

            return $"Registration successful with role: {registration.Role}";
        }

        public async Task<Registration?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }



        public async Task<Registration?> GetUserByRoleAsync(RegisterRole role)
        {
            var user = await _repository.GetByRoleAsync(role);
            if (user == null)
                throw new RegisterUserNotFoundException($"No user found with role {role}");

            return user;
        }
      

    }
}
