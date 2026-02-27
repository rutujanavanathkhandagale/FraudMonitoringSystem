using FraudMonitoringSystem.DTOs.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Admin
{
    [ApiController]

    [Route("api/[controller]")]

    public class RolePermissionsController : ControllerBase

    {

        private readonly IRolePermissionService _service;

        public RolePermissionsController(IRolePermissionService service)

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

        public async Task<IActionResult> Create(RolePermissionCreateDto dto)

            => Ok(await _service.AssignPermissionAsync(dto));

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)

            => Ok(await _service.RemovePermissionAsync(id));

    }

}
