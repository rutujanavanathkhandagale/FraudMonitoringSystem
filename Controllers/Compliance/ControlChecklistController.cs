using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class ControlChecklistController : ControllerBase
{
    private readonly IControlChecklistService _service;
    public ControlChecklistController(IControlChecklistService service)
    {
        _service = service;
    }
    [HttpPost("execute")]
    public async Task<IActionResult> Execute(int caseId, string checkedBy)
    {
        return Ok(await _service.ExecuteChecklist(caseId, checkedBy));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
    [HttpGet("result/{result}")]
    public async Task<IActionResult> GetByResult(string result)
    {
        return Ok(await _service.GetByResultAsync(result));
    }
}