using Microsoft.AspNetCore.Mvc;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudMonitoringSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public CustomerChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        // GET: api/customerchat/{customerId}
        [HttpGet("{customerId}")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChat(long customerId)
        {
            var chat = await _chatService.GetChatByCustomerAsync(customerId);
            return Ok(chat);
        }

        // POST: api/customerchat/send
        [HttpPost("send")]
        public async Task<ActionResult> SendMessage(long customerId, string message)
        {
            try
            {
                await _chatService.SendMessageAsync(customerId, "Customer", message);
                return Ok(new { Message = "Message sent by customer." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
