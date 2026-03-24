using Microsoft.AspNetCore.SignalR;

namespace FraudMonitoringSystem.Models.Notification
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string role, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", role, message);
        }
    }
}
