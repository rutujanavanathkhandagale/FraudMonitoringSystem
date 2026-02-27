using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;
        public RolesController(IRoleService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _service.GetByIdAsync(id));
        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateDto dto)
            => Ok(await _service.CreateAsync(dto));
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoleUpdateDto dto)
            => Ok(await _service.UpdateAsync(id, dto));
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _service.DeleteAsync(id));
    }
}