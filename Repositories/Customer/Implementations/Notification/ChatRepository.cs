using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Exceptions.Notification;
using FraudMonitoringSystem.Models.Notification;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Notification;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations.Notification
{
    public class ChatRepository : IChatRepository
    {
        private readonly WebContext _context;
        public ChatRepository(WebContext context) => _context = context;

        public async Task<int> SendMessageAsync(ChatMessage message)
        {
            _context.ChatMessages.Add(message);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> SaveAttachmentAsync(DocumentAttachment attachment)
        {
            _context.DocumentAttachments.Add(attachment);
            return await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<ChatMessage>> GetMessagesByCustomerIdAsync(int customerId)
     => await _context.ChatMessages.Include(m => m.Attachments)
         .Where(m => m.CustomerId == customerId).ToListAsync();


        public async Task<int> MarkAsSeenAsync(int messageId)
        {
            var msg = await _context.ChatMessages.FindAsync(messageId);
            if (msg == null) throw new CustomNotFoundException("Message not found");
            msg.IsSeen = true;
            return await _context.SaveChangesAsync();
        }
    }

}
