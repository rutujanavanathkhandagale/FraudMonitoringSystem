namespace FraudMonitoringSystem.DTOs.Admin
{
    public class SystemUserResponseDto
    {        // System User info
             public int SystemUserId { get; set; }

        public string SystemUserCode { get; set; } = string.Empty;

        public bool IsApproved { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public int? ApprovedBy { get; set; }


        // Registration (Person) info
        public int RegistrationId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNo { get; set; } = string.Empty;


        // Role info
        public string Role { get; set; } = string.Empty;

    }

}
