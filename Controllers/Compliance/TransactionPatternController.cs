namespace FraudMonitoringSystem.Controllers.Compliance
{
    using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;
    using Microsoft.AspNetCore.Mvc;
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionPatternController : ControllerBase
    {
        private readonly ITransactionPatternService _service;
        public TransactionPatternController(ITransactionPatternService service)
        {
            _service = service;
        }
        [HttpGet("{customerId}")]
        public IActionResult CheckCustomer(int customerId)
        {
            var result = _service.CheckCustomerTransactionPattern(customerId);
            return Ok(result);
        }
    }
}
