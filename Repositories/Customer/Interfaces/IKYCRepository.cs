using FraudMonitoringSystem.Models.Customer;
namespace FraudMonitoringSystem.Repositories.Customer.Interfaces
{
    public interface IKYCRepository
    {
        Task<KYCProfile?> GetByIdAsync(long id);

        Task<KYCProfile?> GetByCustomerIdAsync(long customerId);


        Task<KYCProfile?> VerifyByCustomerIdAsync(long customerId); // new method
        Task<List<KYCProfile>> SearchAsync(string query);
        Task<KYCProfile> AddAsync(KYCProfile profile);

        Task<KYCProfile?> VerifyAsync(KYCProfile profile);
    }
}