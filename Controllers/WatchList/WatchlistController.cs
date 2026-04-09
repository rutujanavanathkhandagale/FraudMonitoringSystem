using FraudMonitoringSystem.DTOs.Watchlist;
using FraudMonitoringSystem.Services.Customer.Interfaces.Watchlist;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WatchlistController : ControllerBase
{
    private readonly IWatchlistService _service;
    public WatchlistController(IWatchlistService service) => _service = service;

    [HttpGet("verify/{customerId}")]
    public async Task<IActionResult> Verify(long customerId)
    {
        // If IsCustomerOnWatchlistAsync is true, the result is a FAIL
        bool isFlagged = await _service.IsCustomerOnWatchlistAsync(customerId);

        return Ok(new
        {
            customerId = customerId,
            status = isFlagged ? "FAIL" : "PASS",
            message = isFlagged
                ? "Match found in Sanctions/PEP records."
                : "Customer is clear of all watchlist flags."
        });
    }

    // CRUD OPERATIONS
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id) => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] WatchlistEntryDto dto)
    {
        await _service.AddAsync(dto);
        return Ok("Added Successfully");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] WatchlistEntryDto dto)
    {
        await _service.UpdateAsync(dto);
        return Ok("Updated Successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteAsync(id);
        return Ok("Deleted Successfully");
    }
}