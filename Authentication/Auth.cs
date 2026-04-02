using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs;
using FraudMonitoringSystem.Models.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace FraudMonitoringSystem.Authentication
{
    public class Auth : IAuth
    {
        private readonly WebContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public Auth(WebContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        // ✅ LOGIN
        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _context.Registrations

            .Include(r => r.UserInfo)

            .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)

                throw new Exception("Invalid Email");

            if (user.UserInfo == null)

                throw new Exception("UserInfo missing");

            if (string.IsNullOrEmpty(user.Password))

                throw new Exception("Password not set");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))

                throw new Exception("Invalid Password");

            var isVerified = user.UserInfo.IsEmailVerified;


            // ✅ Build claims for JWT
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Email ?? string.Empty),
        new Claim(ClaimTypes.Role, user.Role.ToString()),
        new Claim("RegistrationId", user.RegistrationId.ToString()),
        new Claim("isVerified", isVerified.ToString().ToLower())
    };

            // ✅ Create signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // ✅ Generate JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        // ✅ GENERATE OTP
        public async Task<string> GenerateOtpAsync(string email)
        {
            var user = await _context.Registrations
                .Include(r => r.UserInfo)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return null;

            // Generate a 6-digit OTP
            var otp = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

            // Always store expiry in UTC for consistency
            var expiryUtc = DateTime.UtcNow.AddMinutes(10);
            user.UserInfo.VerificationOtp = otp;
            user.UserInfo.OtpExpiry = expiryUtc;

            _context.UserInfos.Update(user.UserInfo);
            await _context.SaveChangesAsync();

            // Convert expiry to local time (IST) for display
            var expiryLocal = expiryUtc.ToLocalTime();

            // Send email with OTP and human-readable expiry
            await _emailService.SendEmailAsync(
                email,
                "Verification Code",
                $"Your OTP is: <b>{otp}</b><br/>" +
                $"It will expire at: {expiryLocal:dd-MM-yyyy hh:mm:ss tt} (local time)."
            );

            return otp;
        }

        // ✅ VERIFY EMAIL
        public async Task<bool> VerifyEmailAsync(VerifyEmailDto dto)
        {
            var user = await _context.Registrations
                .Include(r => r.UserInfo)
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null || user.UserInfo.VerificationOtp != dto.Otp || user.UserInfo.OtpExpiry < DateTime.UtcNow)
                return false;

            user.UserInfo.IsEmailVerified = true;
            user.UserInfo.VerificationOtp = null;
            user.UserInfo.OtpExpiry = null;

            _context.UserInfos.Update(user.UserInfo);
            await _context.SaveChangesAsync();

            return true;
        }

        // ✅ GENERATE RESET TOKEN
        public async Task<string> GenerateResetTokenAsync(string email)
        {
            var user = await _context.Registrations
                .Include(r => r.UserInfo)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return null;

            var token = Guid.NewGuid().ToString();
            user.UserInfo.ResetToken = token;
            user.UserInfo.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(30);

            _context.UserInfos.Update(user.UserInfo);
            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(email, "Password Reset Request",
                $"Use this token to reset your password: <b>{token}</b>. Valid for 30 minutes.");

            return token;
        }

        // ✅ RESET PASSWORD
        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            // 1. Find the user and include UserInfo
            var user = await _context.Registrations
                .Include(u => u.UserInfo)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            // Validate
            if (user == null || user.UserInfo == null) return false;

            // DEBUG: Check if the tokens match in your Visual Studio Output window
            Console.WriteLine($"DB Token: {user.UserInfo.VerificationOtp} | Received: {dto.Token}");

            if (user.UserInfo.VerificationOtp == null || user.UserInfo.VerificationOtp != dto.Token)
                return false;

            if (user.UserInfo.OtpExpiry < DateTime.UtcNow)
                return false;

            //  Update the Registration table (Password)
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            // Update the UserInfo table (Clear the OTP)
            user.UserInfo.VerificationOtp = null;
            user.UserInfo.OtpExpiry = null;

            //  CRITICAL: Tell EF to update BOTH tables
            _context.Entry(user).State = EntityState.Modified;
            _context.Entry(user.UserInfo).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return true;
        }


        // ✅ LOGOUT
        public async Task<bool> LogoutAsync(string email)
        {
            var user = await _context.Registrations
                .Include(r => r.UserInfo)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return false;

            user.UserInfo.ResetToken = null;
            user.UserInfo.VerificationOtp = null;

            _context.UserInfos.Update(user.UserInfo);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}