using FraudMonitoringSystem.Models.ComplianceOfficer;

using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;

using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Customer

{

    [Route("api/[controller]")]

    [ApiController]

    public class SanctionController : ControllerBase

    {

        private readonly ISanctionService _service;

        public SanctionController(ISanctionService service)

        {

            _service = service;

        }

        //  POST - Add to Sanction List

        [HttpPost("add")]

        public async Task<IActionResult> AddSanction([FromBody] Sanction model)

        {

            var result = await _service.AddAsync(model);

            return Ok(result);

        }

        //  GET - Check Screening

        [HttpGet("check/{customerId}")]

        public async Task<IActionResult> CheckSanction(long customerId)

        {

            var result = await _service.CheckSanctionAsync(customerId);

            return Ok(result);

        }

    }

}
