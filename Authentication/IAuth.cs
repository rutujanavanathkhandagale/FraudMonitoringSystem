using FraudMonitoringSystem.DTOs;

namespace FraudMonitoringSystem.Authentication
{
    public interface IAuth
    {
        Task<string> LoginAsync(LoginDto dto);
        Task<string> GenerateOtpAsync(string email);
        Task<bool> VerifyEmailAsync(VerifyEmailDto dto);
        Task<string> GenerateResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
        Task<bool> LogoutAsync(string email);
    }
}