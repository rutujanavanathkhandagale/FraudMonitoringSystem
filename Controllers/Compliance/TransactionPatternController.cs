using Microsoft.AspNetCore.Mvc;
using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;


namespace FraudMonitoringSystem.Controllers.Compliance

{

    [Route("api/[controller]")]

    [ApiController]

    public class TransactionPatternController : ControllerBase

    {

        private readonly ITransactionPatternService _service;

        public TransactionPatternController(ITransactionPatternService service)

        {

            _service = service;

        }

        [HttpGet("{customerId}")]

        public async Task<IActionResult> CheckCustomer(int customerId)

        { 
            var result = await _service.CheckCustomerTransactionPattern(customerId);

            return Ok(result);

        }

    }

}
