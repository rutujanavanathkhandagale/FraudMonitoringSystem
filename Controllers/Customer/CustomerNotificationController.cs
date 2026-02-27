using Microsoft.AspNetCore.Mvc;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudMonitoringSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerNotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public CustomerNotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: api/customer/{customerId}/notifications
        [HttpGet("{customerId}/notifications")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications(long customerId)
        {
            var notifications = await _notificationService.GetUserNotificationsAsync(customerId);
            return Ok(notifications);
        }

        // PUT: api/customer/notifications/{notificationId}/read
        [HttpPut("notifications/{notificationId}/read")]
        public async Task<ActionResult> MarkNotificationAsRead(int notificationId)
        {
            await _notificationService.MarkAsReadAsync(notificationId);
            return Ok(new { Message = "Notification marked as read." });
        }
    }
}
