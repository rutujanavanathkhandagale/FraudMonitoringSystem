using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FraudMonitoringSystem.Controllers.Customer
{
    //[Authorize(Roles = "Customer,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        // Create new account
        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            await _service.CreateAccountAsync(account);
            return Ok(new { Message = "Account created successfully" });
        }

        // Partial update (PATCH)
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(long id, [FromBody] Account partialAccount)
        {
            var updated = await _service.PatchAsync(id, partialAccount);
            return Ok(updated);
        }

        // Get account by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var account = await _service.GetAccountByIdAsync(id);
            return Ok(account);
        }

        // Get accounts by Customer ID (foreign key)
        [HttpGet("by-customer/{customerId:long}")]
        public async Task<IActionResult> GetByCustomerId(long customerId)
        {
            var accounts = await _service.GetAccountsByCustomerIdAsync(customerId);
            return Ok(accounts);
        }

        // Full update (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Account account)
        {
            account.AccountId = id;
            var updated = await _service.UpdateAccountAsync(account);
            return Ok(updated);
        }

        // Delete account
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAccountAsync(id);
            return Ok(new { Message = $"Account {id} deleted successfully" });
        }
    }
}
