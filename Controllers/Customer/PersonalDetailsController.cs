using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Customer
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class PersonalDetailsController : ControllerBase
    {
        private readonly IPersonalDetailsService _service;

        public PersonalDetailsController(IPersonalDetailsService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        
        public async Task<IActionResult> Add([FromBody] PersonalDetails details)
        {
            var result = await _service.AddAsync(details);
            return Ok(result);
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("All")]
       
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        [HttpPut("{id}")]
   
        public async Task<IActionResult> Update(long id, [FromBody] PersonalDetails details)
        {
            if (id != details.CustomerId) return BadRequest("ID in URL and body must match");
            var result = await _service.UpdateAsync(details); return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
     
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
        [HttpPatch("{id}")]
      
        public async Task<IActionResult> Patch(long id, [FromBody] PersonalDetails details)
        {
            var result = await _service.PatchAsync(id, details); return Ok(result);
        }
    }
}
