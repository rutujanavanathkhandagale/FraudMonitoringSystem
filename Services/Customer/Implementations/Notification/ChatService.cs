using FraudMonitoringSystem.DTOs.Notification;
using FraudMonitoringSystem.Models.Notification;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Notification;
using FraudMonitoringSystem.Services.Customer.Interfaces.Notification;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Notification
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepo;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatService(IChatRepository chatRepo, IHubContext<ChatHub> hubContext)
        {
            _chatRepo = chatRepo;
            _hubContext = hubContext;
        }

        public async Task SendMessageAsync(ChatMessageDto dto)
        {
            var message = new ChatMessage
            {
                CustomerId = dto.CustomerId,
                SenderRole = dto.SenderRole,
                MessageText = dto.MessageText,
                SentAt = DateTime.UtcNow // store in UTC for consistency
            };

            var result = await _chatRepo.SendMessageAsync(message);
            if (result == 0) throw new ValidationException("Message could not be sent");

            // ✅ Handle file uploads safely
            if (dto.Files != null && dto.Files.Any())
            {
                // Ensure Uploads folder exists under wwwroot
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                foreach (var file in dto.Files)
                {
                    var fileName = file.FileName;
                    var filePath = Path.Combine(uploadFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var attachment = new DocumentAttachment
                    {
                        ChatMessageId = message.MessageId,
                        FileName = file.FileName,
                        FilePath = $"/Uploads/{fileName}" // relative path for UI
                    };

                    // Add to message object
                    message.Attachments.Add(attachment);

                    // ✅ Persist to DB
                    await _chatRepo.SaveAttachmentAsync(attachment);
                }
            }

            // ✅ Real-time push via SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", dto.SenderRole, dto.MessageText);
        }

        // ✅ Only fetch messages, do not auto-mark as seen
        public async Task<IEnumerable<ChatMessage>> GetMessagesAsync(int customerId)
        {
            return await _chatRepo.GetMessagesByCustomerIdAsync(customerId);
        }

        // ✅ Explicitly mark message as seen
        public async Task MarkMessageSeenAsync(int messageId)
        {
            await _chatRepo.MarkAsSeenAsync(messageId);
        }
    }
}
