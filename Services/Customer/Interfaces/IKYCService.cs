using FraudMonitoringSystem.Models.Customer;
namespace FraudMonitoringSystem.Services.Customer.Interfaces
{
    public interface IKYCService
    {
        Task<KYCProfile?> GetByIdAsync(long id);

        Task<KYCProfile?> GetByCustomerIdAsync(long customerId);

        Task<List<KYCProfile>> SearchAsync(string query);
        Task<KYCProfile> CreateAsync(long customerId, List<IFormFile> documents, List<string> requiredDocs);
        Task<KYCProfile?> VerifyAsync(long id);
        Task<IEnumerable<KYCProfile>> GetAllAsync();
        Task<KYCProfile?> VerifyByCustomerIdAsync(long customerId); // new by CustomerId
    }
}