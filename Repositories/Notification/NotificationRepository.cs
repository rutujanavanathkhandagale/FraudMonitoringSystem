using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace FraudMonitoringSystem.Repositories.Notification
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly WebContext _context;

        public NotificationRepository(WebContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(NotificationEntity notification)
        {
            // Always store UTC in DB
            notification.Timestamp = DateTime.UtcNow;

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }


        public async Task<List<NotificationEntity>> GetByCustomerIdAsync(long customerId)
        {
            return await _context.Notifications
                .Where(n => n.CustomerId == customerId)
                .OrderByDescending(n => n.Timestamp)
                .ToListAsync();
        }

    }
}