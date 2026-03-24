using FraudMonitoringSystem.DTOs.Customer;

namespace FraudMonitoringSystem.Services.Customer.Interfaces
{
    public interface IPersonalDetailsService
    {
   
        Task<CustomerDto> GetByIdAsync(long id);
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto> CreateAsync(CustomerDto dto);
        Task<CustomerDto> UpdateAsync(CustomerDto dto);
        Task DeleteAsync(long id);

     
        Task<List<CustomerDto>> SearchByNameAsync(string name);
        Task<CustomerDto?> GetByEmailAsync(string email);
    }
}
