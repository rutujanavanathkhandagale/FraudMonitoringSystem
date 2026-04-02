using Microsoft.AspNetCore.Mvc;
using FraudMonitoringSystem.Models;

[ApiController]
[Route("api/[controller]")]
public class ControlChecklistController : ControllerBase
{
    private readonly ControlChecklistService _service;
    public ControlChecklistController(ControlChecklistService service) => _service = service;

    [HttpGet] // GET /api/ControlChecklist?status=PASS
    public async Task<IActionResult> GetHistory([FromQuery] string status = "ALL") => Ok(await _service.GetHistoryAsync(status));

    [HttpPost] // POST /api/ControlChecklist
    public async Task<IActionResult> Create([FromBody] ControlChecklist checklist)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var res = await _service.CreateAnalysisAsync(checklist);
            return CreatedAtAction(nameof(GetHistory), new { id = res.Id }, res);
        }
        catch (ArgumentException ex) { return BadRequest(new { m = ex.Message }); }
        catch (KeyNotFoundException ex) { return NotFound(new { m = ex.Message }); }
    }

    [HttpPut("{caseId}")] // PUT /api/ControlChecklist/101
    public async Task<IActionResult> Update(int caseId, [FromBody] List<ControlDetail> details)
    {
        var res = await _service.UpdateAnalysisAsync(caseId, details);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpDelete("{caseId}")] // DELETE /api/ControlChecklist/101
    public async Task<IActionResult> Delete(int caseId) =>
        await _service.RemoveAnalysisAsync(caseId) ? NoContent() : NotFound();
}