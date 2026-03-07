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

    [HttpPost]

    public IActionResult Create(ControlChecklist checklist)

    {

        return Ok(_service.Add(checklist));

    }

    [HttpGet]

    public IActionResult GetAll()

    {

        return Ok(_service.GetAll());

    }

    [HttpGet("case/{caseId}")]

    public IActionResult GetByCaseId(int caseId)

    {

        var result = _service.GetByCaseId(caseId);

        if (result == null)

            return NotFound();

        return Ok(result);

    }

    [HttpGet("status/{status}")]

    public IActionResult GetByStatus(string status)

    {

        return Ok(_service.GetByStatus(status));

    }

    [HttpPut]

    public IActionResult Update(ControlChecklist checklist)

    {

        var result = _service.Update(checklist);

        if (result == null)

            return NotFound();

        return Ok(result);

    }

    [HttpDelete("{checklistId}")]

    public IActionResult Delete(int checklistId)

    {

        if (!_service.Delete(checklistId))

            return NotFound();

        return Ok("Deleted Successfully");

    }

}

