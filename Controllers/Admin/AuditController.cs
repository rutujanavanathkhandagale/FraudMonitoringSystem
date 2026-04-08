using FraudMonitoringSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Controllers.Admin
{
    [ApiController]

    [Route("api/admin/audit")]

    public class AuditController : ControllerBase
    {
        private readonly WebContext _context;


        public AuditController(WebContext context)

        {
            _context = context;

        }

        // ✅ GET AUDIT LOGS FOR SYSTEM USER
        [HttpGet("system-user/{systemUserId}")]

        public async Task<IActionResult> GetSystemUserAuditLogs(int systemUserId)

        {
            var logs = await _context.AuditLogs

                .Where(a => a.EntityType == "SystemUser" && a.EntityId == systemUserId)

                .OrderByDescending(a => a.PerformedAt)

                .ToListAsync();


            return Ok(logs);

        }        // ✅ GET ALL AUDIT LOGS (ADMIN VIEW)
                 [HttpGet("all")]

        public async Task<IActionResult> GetAllAuditLogs()

        {

            var logs = await _context.AuditLogs

                .OrderByDescending(a => a.PerformedAt)

                .ToListAsync();


            return Ok(logs);

        }


    }

}

 

