using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FraudMonitoringSystem.Models.Customer
{
    public class NotificationHub : Hub
    {
        public async Task SendNotificationToCustomer(int customerId, string message, string category)
        {
            await Clients.User(customerId.ToString())
                         .SendAsync("ReceiveNotification", message, category);
        }
    }
}
