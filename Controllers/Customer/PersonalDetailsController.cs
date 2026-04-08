using FraudMonitoringSystem.DTOs.Customer;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Customer
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalDetailsController : ControllerBase
    {
        private readonly IPersonalDetailsService _service;

        public PersonalDetailsController(IPersonalDetailsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { Message = $"Customer {id} not found" });

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            if (list == null || !list.Any())
                return NotFound(new { Message = "No customers found" });

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto dto)
        {
            var created = await _service.CreateAsync(dto);
            if (created == null)
                return BadRequest(new { Message = "Failed to create customer" });

            return Ok(created);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var result = await _service.SearchByNameAsync(name);
            if (result == null || !result.Any())
                return NotFound(new { Message = $"No customers found with name {name}" });

            return Ok(result);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _service.GetByEmailAsync(email);
            if (result == null)
                return NotFound(new { Message = $"Customer with email {email} not found" });

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, CustomerDto dto)
        {
            dto.CustomerId = id;
            var updated = await _service.UpdateAsync(dto);
            if (updated == null)
                return NotFound(new { Message = $"Customer {id} not found" });

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { Message = $"Customer {id} not found" });

            return Ok(new { Message = $"Customer {id} deleted successfully" });
        }
    }
}
