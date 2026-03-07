using FraudMonitoringSystem.Aspects.Customer;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FraudMonitoringSystem.Services.Customer.Interfaces;

namespace FraudMonitoringSystem.Controllers.Customer
{
    //[Authorize(Roles = "Customer,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    [TypeFilter(typeof(AccountExceptionHandler))]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            var result = await _service.CreateAccountAsync(account);
            return Ok(new { Message = "Account created successfully", RowsAffected = result });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(long id, [FromBody] Account partialAccount)
        {
            var updated = await _service.PatchAsync(id, partialAccount);
            return Ok(updated);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var account = await _service.GetAccountByIdAsync(id);
            return Ok(account);
        }

        // ✅ New: GET /api/Account/by-customer/{customerId}
        [HttpGet("by-customer/{customerId:long}")]
        public async Task<IActionResult> GetByCustomerId(long customerId)
        {
            var accounts = await _service.GetAccountsByCustomerIdAsync(customerId);
            return Ok(accounts); // returns IEnumerable<Account>
        }
    }
}