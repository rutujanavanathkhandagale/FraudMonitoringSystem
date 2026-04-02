using Microsoft.AspNetCore.Mvc;
using FraudMonitoringSystem.DTOs;
using FraudMonitoringSystem.Services.Interfaces;
using FraudMonitoringSystem.Models.Customer;

namespace FraudMonitoringSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _service;

        public RegistrationController(IRegistrationService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registration)
        {
            try
            {
                // Call the service which now contains the 'cleanedRole' logic
                var dto = await _service.RegisterAsync(registration);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                // Return a 400 Bad Request instead of crashing the server
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetUserByRole(RegisterRole role)
        {
            var dto = await _service.GetUserByRoleAsync(role);
            return Ok(dto); // ✅ Only DTO fields returned
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            var roles = Enum.GetNames(typeof(RegisterRole));
            return Ok(roles);
        }
    }
}