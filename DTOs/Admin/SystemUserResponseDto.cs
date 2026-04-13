namespace FraudMonitoringSystem.DTOs.Admin
{
    public class SystemUserResponseDto
    {
        public int SystemUserId { get; set; }
        public string SystemUserCode { get; set; } = string.Empty;

        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }

        public DateTime? ApprovedAt { get; set; }
        public int? ApprovedBy { get; set; }

        public int RegistrationId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;

        // ✅ role info
        public string? Role { get; set; }

        // ✅ removed role info
        public string? LastAssignedRoleId { get; set; }
        public string? LastAssignedRoleName { get; set; }
    }
}