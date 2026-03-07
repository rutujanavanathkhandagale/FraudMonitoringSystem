using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;

using FraudMonitoringSystem.Models.ComplianceOfficer;

using Microsoft.AspNetCore.Mvc;

using FraudMonitoringSystem.Models.ComplianceOfficer;

using Microsoft.AspNetCore.Mvc;

using FraudMonitoringSystem.Models.ComplianceOfficer;

[ApiController]

[Route("api/[controller]")]

public class RegulatoryReportController : ControllerBase

{

    private readonly IRegulatoryReportService _service;

    public RegulatoryReportController(IRegulatoryReportService service)

    {

        _service = service;

    }

    [HttpPost("{customerId}")]

    public IActionResult GenerateReport(int customerId)

    {

        var result = _service.GenerateReport(customerId);

        return Ok(result);

    }

    [HttpGet("customer/{customerId}")]

    public IActionResult GetByCustomer(int customerId)

    {

        return Ok(_service.GetByCustomerId(customerId));

    }

    [HttpGet("status/{status}")]

    public IActionResult GetByStatus(string status)

    {

        return Ok(_service.GetByStatus(status));

    }

    [HttpPut("{id}")]

    public IActionResult Update(int id, Regulatory_Report report)

    {

        return Ok(_service.UpdateReport(id, report));

    }

    [HttpDelete("{id}")]

    public IActionResult Delete(int id)

    {

        return Ok(_service.DeleteReport(id));

    }

}

