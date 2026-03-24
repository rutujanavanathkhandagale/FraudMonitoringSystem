namespace FraudMonitoringSystem.Controllers.WatchList
{
  
    using global::FraudMonitoringSystem.DTOs.Watchlist;
    using global::FraudMonitoringSystem.Services.Customer.Interfaces.Watchlist;
    using Microsoft.AspNetCore.Mvc;

    namespace FraudMonitoringSystem.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class EntityLinkController : ControllerBase
        {
            private readonly IEntityLinkService _service;

            public EntityLinkController(IEntityLinkService service)
            {
                _service = service;
            }

            [HttpGet("customer/{id}")]
            public async Task<IActionResult> GetLinksByCustomer(long id) =>
                Ok(await _service.GetLinksByCustomerIdAsync(id));

            [HttpGet("account/{id}")]
            public async Task<IActionResult> GetLinksByAccount(long id) =>
                Ok(await _service.GetLinksByAccountIdAsync(id));

            [HttpPost]
            public async Task<IActionResult> AddLink([FromBody] EntityLinkDto dto)
            {
                await _service.AddLinkAsync(dto);
                return Ok("Entity link added successfully");
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteLink(long id)
            {
                await _service.DeleteLinkAsync(id);
                return Ok("Entity link deleted successfully");
            }
        }
    }
}

