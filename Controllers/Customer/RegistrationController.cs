using Microsoft.AspNetCore.Mvc;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Services.Interfaces;

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
        public async Task<IActionResult> Register([FromBody] Registration registration)
        {
            var response = await _service.RegisterAsync(registration);
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var profile = await _service.GetByIdAsync(id);
            if (profile == null) return NotFound();

            return Ok(new
            {
                profile.RegistrationId,
                profile.FirstName,
                profile.LastName,
                profile.Email
            });
        }


        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetUserByRole(RegisterRole role)
        {
            var user = await _service.GetUserByRoleAsync(role);
            return Ok(user);
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            var roles = Enum.GetNames(typeof(RegisterRole));
            return Ok(roles);
            
        }
     
    }
}
