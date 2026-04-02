namespace FraudMonitoringSystem.DTOs
{
    public record VerifyEmailDto
    {

        public string Email { get; set; }

        public string Otp { get; set; }

    }

}
