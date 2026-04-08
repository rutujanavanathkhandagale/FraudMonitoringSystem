using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.DTOs.Admin
{
    public class RoleUpdateDto
    {
        public string RoleName { get; set; } = string.Empty;

        public string? Description { get; set; }

    }

}
