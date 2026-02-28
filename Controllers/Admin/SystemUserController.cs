using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Controllers.Admin
{
    [Route("api/system-users")]
    [ApiController]
    public class SystemUsersController : ControllerBase
    {
        private readonly ISystemUserService _service;
        public SystemUsersController(ISystemUserService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var result = await _service.GetAllAsync(page, pageSize);
            return Ok(result);
        }
        [HttpGet("by-role/{role}")]
        public async Task<IActionResult> GetByRole(AdminRole role)
        {
            var result = await _service.GetByRoleAsync(role);
            return Ok(result);
        }
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody]SystemUserCreateDto dto)
        //{
        //    await _service.AddAsync(dto);
        //    return StatusCode(201);
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}