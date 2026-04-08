using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Controllers.Admin
{
    [ApiController]

    [Route("api/system-users")]

    public class SystemUsersController : ControllerBase
    {
        private readonly ISystemUserService _service;


        public SystemUsersController(ISystemUserService service)

        {
            _service = service;

        }

        // ✅ Get all system users (paged)// GET: api/system-users?page=1&pageSize=10
        [HttpGet]

        public async Task<IActionResult> GetAll(

            int page = 1,

            int pageSize = 10)

        {
            var users = await _service.GetAllAsync(page, pageSize);

            return Ok(users);

        }

        // ✅ Get system users by role// GET: api/system-users/by-role/2
        [HttpGet("by-role/{roleId:int}")]

        public async Task<IActionResult> GetByRoleId(string roleId)

        {
            var users = await _service.GetByRoleIdAsync(roleId);

            return Ok(users);

        }

        // ✅ Create system user (will generate B301, B302...)// POST: api/system-users
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] SystemUserCreateDto dto)

        {
            await _service.AddAsync(dto);

            return StatusCode(201, new { Message = "System user created successfully" });
        }

        // ✅ Delete system user// DELETE: api/system-users/10
        [HttpDelete("{id:int}")]

        public async Task<IActionResult> Delete(int id)

        {
            await _service.DeleteAsync(id);

            return NoContent();

        }

        // ✅ Approve system user (Admin action)// POST: api/system-users/10/approve?adminRegistrationId=1
        [HttpPost("{id:int}/approve")]

        public async Task<IActionResult> Approve(

            int id,

            [FromQuery] int adminRegistrationId)

        {

            await _service.ApproveAsync(id, adminRegistrationId);


            return Ok(new

            {

                Message = "System user approved successfully"

            });

        }

        // ✅ Get system user by id (PROFILE VIEW)// GET: api/system-users/10

        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetById(int id)

        {

            var user = await _service.GetByIdAsync(id);

            return Ok(user);

        }

    }

}