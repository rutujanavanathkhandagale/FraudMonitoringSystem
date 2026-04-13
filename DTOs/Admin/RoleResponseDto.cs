namespace FraudMonitoringSystem.DTOs.Admin
{
    public class RoleResponseDto
    {
        public string  RoleId { get; set; }
        public string RoleName { get; set; }
         public int ActiveUserCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}