using FraudMonitoringSystem.Models;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces
{
    public interface INotificationRepository
    {
        Task SaveAsync(NotificationEntity notification);
        Task<List<NotificationEntity>> GetByCustomerIdAsync(long customerId);
    }
}
