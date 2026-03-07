using FraudMonitoringSystem.Models.ComplianceOfficer;

using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;

using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Customer

{

    [Route("api/[controller]")]

    [ApiController]

    public class PEPListController : ControllerBase

    {

        private readonly IPEPListService _service;

        public PEPListController(IPEPListService service)

        {

            _service = service;

        }

        // 🔹 POST - Add to PEP List

        [HttpPost]

        public async Task<IActionResult> AddPep([FromBody] PEPListModel model)

        {

            var result = await _service.AddAsync(model);

            return Ok(result);

        }

       //GET - Check PEP Screening

        [HttpGet("check/{customerId}")]

        public async Task<IActionResult> CheckPep(long customerId)

        {

            var result = await _service.CheckPepAsync(customerId);

            return Ok(result);

        }

    }

}
