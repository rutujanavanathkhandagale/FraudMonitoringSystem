using FraudMonitoringSystem.Models;

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

    // ================= GET ALL =================

    [HttpGet]

    public async Task<IActionResult> GetHistory([FromQuery] string status = "ALL")

    {

        var result = await _service.GetHistoryAsync(status);

        return Ok(result ?? new List<ControlChecklist>());

    }

    // ================= GET BY CASE =================

    [HttpGet("{caseId}")]

    public async Task<IActionResult> GetByCaseId(int caseId)

    {

        var result = await _service.GetHistoryAsync("ALL");

        var data = result.FirstOrDefault(x => x.CaseID == caseId);

        if (data == null) return NotFound();

        return Ok(data);

    }

    // ================= CREATE =================
    [HttpPost]
    public async Task<IActionResult> SaveChecklist([FromBody] ControlChecklist request)
    {
        try
        {
            if (request == null) return BadRequest("Invalid request");

            // The Service we updated in Step 2 will now force the FAIL status
            var result = await _service.CreateAnalysisAsync(request);

            return Ok(new
            {
                message = "Checklist created successfully",
                data = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // ================= UPDATE =================

    [HttpPut("{caseId}")]

    public async Task<IActionResult> Update(int caseId, [FromBody] List<ControlDetail> details)

    {

        var res = await _service.UpdateAnalysisAsync(caseId, details);

        if (res == null) return NotFound();

        return Ok(res);

    }

    // ================= DELETE =================

    [HttpDelete("{caseId}")] 
    public async Task<IActionResult> Delete([FromRoute] int caseId)
    {
        var deleted = await _service.RemoveAnalysisAsync(caseId);
        if (!deleted) return NotFound(new { message = $"Analysis with ID {caseId} not found." });

        return NoContent(); 
    }

}
