namespace FraudMonitoringSystem.Hub
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    public class NotificationHub : Hub
    {
        public async Task SendNotification(string customerId, string message)
        {
            // Push notification to all clients in the group
            await Clients.Group(customerId).SendAsync("ReceiveNotification", message);
        }

        public override async Task OnConnectedAsync()
        {
            // Client connects with ?customerId=123
            var customerId = Context.GetHttpContext().Request.Query["customerId"];
            if (!string.IsNullOrEmpty(customerId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, customerId);
            }
            await base.OnConnectedAsync();
        }
    }
}

