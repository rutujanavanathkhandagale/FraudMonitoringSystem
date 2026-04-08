using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Admin
{
    [ApiController]

    [Route("api/roles")]

    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;


        public RoleController(IRoleService service)

        {
            _service = service;

        }

        [HttpGet]

        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());


        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(string id) =>
            Ok(await _service.GetByIdAsync(id));


        [HttpPost]

        public async Task<IActionResult> Create(RoleCreateDto dto) =>
            Ok(await _service.CreateAsync(dto));


        [HttpPut("{id}")]

        public async Task<IActionResult> Update(string id, RoleUpdateDto dto) =>
            Ok(await _service.UpdateAsync(id, dto));


        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(string id)

        {

            await _service.DeleteAsync(id);

            return Ok("Role deleted successfully");

        }

    }

}
