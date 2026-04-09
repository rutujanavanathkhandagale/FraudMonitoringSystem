using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TransactionPatternController : ControllerBase
{
    private readonly ITransactionPatternService _service;
    public TransactionPatternController(ITransactionPatternService service) => _service = service;

    [HttpGet("{customerId}/{transactionID}")]

    public async Task<IActionResult> Analyze(int customerId, int transactionID)

    {

        var result = await _service.AnalyzeAsync(customerId, transactionID);

        if (result == null)

            return NotFound();

        return Ok(result);

    }


}