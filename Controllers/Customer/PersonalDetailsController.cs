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
        public async Task<IActionResult> Get(long id) => Ok(await _service.GetByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto dto) => Ok(await _service.CreateAsync(dto));

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var result = await _service.SearchByNameAsync(name);
            return Ok(result);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _service.GetByEmailAsync(email);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, CustomerDto dto)
        {
            dto.CustomerId = id;
            return Ok(await _service.UpdateAsync(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { Message = $"Customer {id} deleted successfully" });
        }
    }
}
