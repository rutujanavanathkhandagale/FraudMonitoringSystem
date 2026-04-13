namespace FraudMonitoringSystem.Services.Customer.Interfaces.Common
{
    public interface IAuditLogService
    {

        Task LogAsync(
                    string entityType,
                    string entityId,
                    string action,
                    string description,
                    int performedBy,
                    string? performedByName = null
                );

    }
}
