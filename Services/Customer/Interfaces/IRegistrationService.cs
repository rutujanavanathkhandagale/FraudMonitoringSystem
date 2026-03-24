using FraudMonitoringSystem.Models.Customer;
using System.Threading.Tasks;

namespace FraudMonitoringSystem.Services.Interfaces
{
 
    public interface IRegistrationService
    {
        Task<string> RegisterAsync(Registration registration);
        Task<Registration?> GetUserByRoleAsync(RegisterRole role);

        Task<Registration?> GetByIdAsync(int id);

    }
}
