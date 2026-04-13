using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/system-users")]
    public class SystemUsersController : ControllerBase
    {
        private readonly ISystemUserService _service;

        public SystemUsersController(ISystemUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
            => Ok(await _service.GetAllAsync(page, pageSize));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(SystemUserCreateDto dto)
        {
            await _service.AddAsync(dto);
            return StatusCode(201, new { message = "System user created successfully" });
        }

        [HttpPost("{id:int}/approve")]
        public async Task<IActionResult> Approve(
            int id,
            [FromQuery] int adminRegistrationId)
        {
            await _service.ApproveAsync(id, adminRegistrationId);
            return Ok(new { message = "User approved successfully" });
        }

        [HttpPut("{id:int}/deactivate")]
        public async Task<IActionResult> DeactivateSystemUser(int id)
        {
            int adminRegistrationId = 1; // later from JWT
            await _service.DeactivateAsync(id, adminRegistrationId);
            return Ok(new { message = "User deactivated successfully" });
        }

        // ✅ ✅ CHANGE ROLE
        [HttpPut("{id:int}/change-role")]
        public async Task<IActionResult> ChangeRole(
            int id,
            [FromBody] ChangeRoleDto dto)
        {
            int adminRegistrationId = 1; // later from JWT

            await _service.ChangeRoleAsync(
                id,
                dto.NewRoleId,
                adminRegistrationId
            );

            return Ok(new { message = "Role updated successfully" });
        }
    }
}