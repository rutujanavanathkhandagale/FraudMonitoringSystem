using FraudMonitoringSystem.Hub;
using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FraudMonitoringSystem.Controllers.Notification
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationRepository _repo;

        public NotificationController(IHubContext<NotificationHub> hubContext, INotificationRepository repo)
        {
            _hubContext = hubContext;
            _repo = repo;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            if (request.CustomerId <= 0)
                return BadRequest("CustomerId is required");

            // Save to DB
            var entity = new NotificationEntity
            {
                CustomerId = request.CustomerId,
                Message = request.Message,
                Timestamp = DateTime.UtcNow
            };
            await _repo.SaveAsync(entity);

            // Broadcast live
            await _hubContext.Clients.Group(request.CustomerId.ToString())
                .SendAsync("ReceiveNotification", request.Message);

            return Ok(new { message = "Notification sent successfully" });
        }

        [HttpGet("{customerId:long}")]
        public async Task<IActionResult> GetNotifications(long customerId)
        {
            var notifications = await _repo.GetByCustomerIdAsync(customerId);
            return Ok(notifications);
        }
    }

    public class NotificationRequest
    {
        public long CustomerId { get; set; }   // long type
        public string Message { get; set; }
    }
}
