using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TransactionPatternController : ControllerBase
{
    private readonly ITransactionPatternService _service;
    public TransactionPatternController(ITransactionPatternService service) => _service = service;

    [HttpGet("{customerId}")]
    public async Task<IActionResult> Get(int customerId)
    {
        var result = await _service.AnalyzePatternAsync(customerId);
        if (result == null) return NotFound(new { message = "Customer entity not found." });
        return Ok(result);
    }
}