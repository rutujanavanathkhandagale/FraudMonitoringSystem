using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Common;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Common
{
    public class AuditLogService : IAuditLogService
    {
        private readonly WebContext _context;

        public AuditLogService(WebContext context)
        {
            _context = context;
        }

       public async Task LogAsync(
     string entityType,
     string entityId,
     string action,
     string description,
     int performedBy,
      string? performedByName = null)

        {
            var audit = new AuditLog
            {
                EntityType = entityType,
                EntityId = entityId, // ✅ REAL ID
                Action = action,
                Description = description,
                PerformedBy = performedBy,
                PerformedByName = performedByName,
                PerformedAt = DateTime.UtcNow
            };

            _context.AuditLogs.Add(audit);
            await _context.SaveChangesAsync();
        }
    }
}