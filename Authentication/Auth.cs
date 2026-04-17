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




            var claims = new[]

            {
                new Claim(ClaimTypes.Name, user.Email),

                new Claim(ClaimTypes.Name, user.Email ?? string.Empty),

                 new Claim(ClaimTypes.Role, user.Role.ToString()),

                 new Claim("RegistrationId", user.RegistrationId.ToString()),

                   new Claim("isVerified", isVerified.ToString().ToLower())

};



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken(

                issuer: _configuration["Jwt:Issuer"],

                audience: _configuration["Jwt:Audience"],

                claims: claims,

                expires: DateTime.UtcNow.AddMinutes(120),

                signingCredentials: creds);


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

        // ✅ GENERATE RESET TOKEN (Updated with professional Email Link)
        public async Task<string> GenerateResetTokenAsync(string email)
        {
            var user = await _context.Registrations
                .Include(r => r.UserInfo)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || user.UserInfo == null) return null;

            // 1. Generate unique token
            var token = Guid.NewGuid().ToString();
            user.UserInfo.ResetToken = token;
            user.UserInfo.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(30);

            _context.UserInfos.Update(user.UserInfo);
            await _context.SaveChangesAsync();

            // 2. Create the link for your React frontend (Match your friend's logic)
            var resetLink = $"https://localhost:5173/reset-password?token={token}&email={email}";

            // 3. Professional HTML Body
            var emailBody = $@"
        <div style='font-family: sans-serif; padding: 20px; border: 1px solid #e2e8f0; border-radius: 8px;'>
            <h2 style='color: #d000f5;'>FraudShield Password Reset</h2>
            <p>We received a request to reset your password for the secure terminal. Click the button below to proceed:</p>
            <a href='{resetLink}' 
               style='display: inline-block; background-color: #d000f5; color: white; padding: 12px 24px; 
               text-decoration: none; border-radius: 6px; font-weight: bold; margin: 15px 0;'>
               Reset Password
            </a>
            <p style='font-size: 0.875rem; color: #64748b;'>
                This link expires in 30 minutes. If you didn't request this, you can safely ignore this email.
            </p>
        </div>";

            await _emailService.SendEmailAsync(email, "Reset Your FraudShield Password", emailBody);

            return token;
        }

        // ✅ RESET PASSWORD (Logic Correction & Clean-up)

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            // 1. Find the user in the Registrations table using the email provided
            var user = await _context.Registrations
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null) return false;

            // 2. Hash the new password 
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            // 3. Update the table and SAVE
            _context.Registrations.Update(user);
            await _context.SaveChangesAsync(); // This line stores it in the SQL database

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