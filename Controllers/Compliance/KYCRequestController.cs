using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;
using Microsoft.AspNetCore.Mvc;

[ApiController]

[Route("api/[controller]")]

public class KYCRequestController : ControllerBase

{

    private readonly IKYCRequestService _service;

    public KYCRequestController(IKYCRequestService service)

    {

        _service = service;

    }

    [HttpGet("status/{customerId}")]

    public async Task<IActionResult> GetStatus(long customerId)

    {

        var profile = await _service.GetByCustomerIdAsync(customerId);

        if (profile == null)

            return NotFound();

        return Ok(new

        {

            CustomerId = customerId,

            Status = profile.Status,

            IsVerified = profile.Status == "Verified"

        });

    }

}
