using FraudMonitoringSystem.Models.Notification;

namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.Notification
{
    public interface IChatRepository
    {
        Task<int> SendMessageAsync(ChatMessage message);
        Task<IEnumerable<ChatMessage>> GetMessagesByCustomerIdAsync(int customerId);
        Task<int> MarkAsSeenAsync(int messageId);
        Task<int> SaveAttachmentAsync(DocumentAttachment attachment);

    }

}
