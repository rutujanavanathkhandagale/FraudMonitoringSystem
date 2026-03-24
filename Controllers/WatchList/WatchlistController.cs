using FraudMonitoringSystem.DTOs.Watchlist;
using FraudMonitoringSystem.Services.Customer.Interfaces.Watchlist;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.WatchList
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistService _service;

        public WatchlistController(IWatchlistService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WatchlistEntryDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Watchlist entry added successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WatchlistEntryDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok("Watchlist entry updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return Ok("Watchlist entry deleted successfully");
        }
    }

}
