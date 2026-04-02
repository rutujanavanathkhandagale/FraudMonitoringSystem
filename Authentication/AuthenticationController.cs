using FraudMonitoringSystem.Authentication;
using FraudMonitoringSystem.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        // ✅ LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _auth.LoginAsync(dto);
         

            // This line will now execute because the code no longer "breaks" on the line above
            if (token == null)
                return Unauthorized(new { Message = "Invalid email or password." });

            return Ok(new { Token = token });
        }

        // ✅ GENERATE OTP
        [HttpPost("generate-otp")]
        public async Task<IActionResult> GenerateOtp([FromBody] EmailRequest request)
        {
            var otp = await _auth.GenerateOtpAsync(request.Email);
            if (otp == null) return NotFound(new { Message = "User not found." });

            return Ok(new { Message = "Verification code sent to your email." });
        }

        // ✅ VERIFY EMAIL
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto dto)
        {
            var result = await _auth.VerifyEmailAsync(dto);
            if (!result) return BadRequest(new { Message = "Invalid or expired OTP." });

            return Ok(new { Message = "Email verified successfully." });
        }

        // ✅ GENERATE RESET TOKEN
        [HttpPost("forgot-password")]

        public async Task<IActionResult> ForgotPassword([FromBody] EmailRequest request)

        {

            var otp = await _auth.GenerateOtpAsync(request.Email);

            if (otp == null)

                return NotFound(new { Message = "User not found." });

            return Ok(new { Message = "OTP sent to your email." });

        }


        // ✅ RESET PASSWORD
        [HttpPost("reset-password")]

        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)

        {

            var result = await _auth.ResetPasswordAsync(dto);

            if (!result)

                return BadRequest(new { Message = "Invalid OTP or expired." });

            return Ok(new { Message = "Password updated successfully." });

        }


        // ✅ LOGOUT
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] EmailRequest request)
        {
            var result = await _auth.LogoutAsync(request.Email);
            if (!result) return NotFound(new { Message = "User not found." });

            return Ok(new { Message = "Logged out successfully." });
        }
    }
}