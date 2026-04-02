using FraudMonitoringSystem.Authentication;

namespace FraudMonitoringSystem.Models.Customer
{
    public class UserInfo
    {
        
            public int UserInfoId { get; set; } //primary key
            public int RegistrationId { get; set; }   // foreign key to Registrations
            public bool? IsEmailVerified { get; set; } = false;
            public string? VerificationOtp { get; set; }
            public DateTime? OtpExpiry { get; set; }
            public string? ResetToken { get; set; }
            public DateTime? ResetTokenExpiry { get; set; }

            public Registration Registration { get; set; } // navigation property
        

    }
}
