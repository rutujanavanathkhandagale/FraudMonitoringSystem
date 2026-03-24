using FraudMonitoringSystem.DTOs.Notification;
using FraudMonitoringSystem.Models.Notification;

namespace FraudMonitoringSystem.Services.Customer.Interfaces.Notification
{
    public interface IChatService
    {
        Task SendMessageAsync(ChatMessageDto dto);
        Task<IEnumerable<ChatMessage>> GetMessagesAsync(int customerId);
        Task MarkMessageSeenAsync(int messageId);
    }
}
