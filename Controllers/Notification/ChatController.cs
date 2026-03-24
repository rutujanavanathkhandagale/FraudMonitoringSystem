using FraudMonitoringSystem.DTOs.Notification;
using FraudMonitoringSystem.Services.Customer.Interfaces.Notification;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Notification
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService) => _chatService = chatService;

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromForm] ChatMessageDto dto)
        {
            await _chatService.SendMessageAsync(dto);
            return Ok("Message sent successfully.");
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetMessages(int customerId)
        {
            var messages = await _chatService.GetMessagesAsync(customerId);
            return Ok(messages);
        }

        [HttpPut("seen/{messageId}")]
        public async Task<IActionResult> MarkSeen(int messageId)
        {
            await _chatService.MarkMessageSeenAsync(messageId);
            return Ok("Message marked as seen.");
        }
    }
}
