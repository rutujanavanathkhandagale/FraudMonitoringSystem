using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;
        public PermissionController(IPermissionService service)
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
        public async Task<IActionResult> Create(PermissionCreateDto dto)
            => Ok(await _service.CreateAsync(dto));
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PermissionUpdateDto dto)
            => Ok(await _service.UpdateAsync(id, dto));
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _service.DeleteAsync(id));
    }
}