using FraudMonitoringSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/audit")]
    public class AuditController : ControllerBase
    {
        private readonly WebContext _context;

        public AuditController(WebContext context)
        {
            _context = context;
        }

        // ✅ GET ALL AUDIT LOGS
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAuditLogs()
        {
            var logs = await _context.AuditLogs
                .OrderByDescending(a => a.PerformedAt)
                .Select(a => new
                {
                    a.EntityType,
                    a.EntityId,
                    a.Action,
                    a.Description,
                    PerformedBy = a.PerformedByName != null
                        ? $"{a.PerformedByName} ({a.PerformedBy})"
                        : a.PerformedBy.ToString(),
                    a.PerformedAt
                })
                .ToListAsync();

            return Ok(logs);
        }

        // ✅ GET ROLE AUDIT LOGS
        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoleAuditLogs()
        {
            var logs = await _context.AuditLogs
                .Where(a => a.EntityType == "Role")
                .OrderByDescending(a => a.PerformedAt)
                .Select(a => new
                {
                    a.EntityId,
                    a.Action,
                    a.Description,
                    PerformedBy = a.PerformedByName != null
                        ? $"{a.PerformedByName} ({a.PerformedBy})"
                        : a.PerformedBy.ToString(),
                    a.PerformedAt
                })
                .ToListAsync();

            return Ok(logs);
        }

        // ✅ GET SYSTEM USER AUDIT LOGS
        [HttpGet("system-users")]
        public async Task<IActionResult> GetAllSystemUserAuditLogs()
        {
            var logs = await _context.AuditLogs
                .Where(a => a.EntityType == "SystemUser")
                .OrderByDescending(a => a.PerformedAt)
                .Select(a => new
                {
                    a.EntityId,
                    a.Action,
                    a.Description,
                    PerformedBy = a.PerformedByName != null
                        ? $"{a.PerformedByName} ({a.PerformedBy})"
                        : a.PerformedBy.ToString(),
                    a.PerformedAt
                })
                .ToListAsync();

            return Ok(logs);
        }

        // ✅ GET AUDIT LOGS BY SYSTEM USER ID
        [HttpGet("system-user/{systemUserId}")]
        public async Task<IActionResult> GetSystemUserAuditLogs(int systemUserId)
        {
            var logs = await _context.AuditLogs
                .Where(a =>
                    a.EntityType == "SystemUser" &&
                    a.EntityId == systemUserId.ToString())
                .OrderByDescending(a => a.PerformedAt)
                .Select(a => new
                {
                    a.Action,
                    a.Description,
                    PerformedBy = a.PerformedByName != null
                        ? $"{a.PerformedByName} ({a.PerformedBy})"
                        : a.PerformedBy.ToString(),
                    a.PerformedAt
                })
                .ToListAsync();

            return Ok(logs);
        }

        // ✅ ✅ ROLE CHANGE HISTORY (IMPORTANT)
        [HttpGet("system-user/{systemUserId}/role-history")]
        public async Task<IActionResult> GetSystemUserRoleHistory(int systemUserId)
        {
            var logs = await _context.AuditLogs
                .Where(a =>
                    a.EntityType == "SystemUser" &&
                    a.EntityId == systemUserId.ToString() &&
                    a.Action == "ROLE_CHANGE")
                .OrderByDescending(a => a.PerformedAt)
                .Select(a => new
                {
                    a.Description,
                    PerformedBy = a.PerformedByName != null
                        ? $"{a.PerformedByName} ({a.PerformedBy})"
                        : a.PerformedBy.ToString(),
                    a.PerformedAt
                })
                .ToListAsync();

            return Ok(logs);
        }
    }
}