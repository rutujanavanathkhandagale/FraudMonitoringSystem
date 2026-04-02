using FraudMonitoringSystem.DTOs;
using FraudMonitoringSystem.Models.Customer;
using System.Threading.Tasks;

namespace FraudMonitoringSystem.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegistrationDto> RegisterAsync(RegistrationDto registration);
        Task<RegistrationDto> GetUserByRoleAsync(RegisterRole role);
    }
}