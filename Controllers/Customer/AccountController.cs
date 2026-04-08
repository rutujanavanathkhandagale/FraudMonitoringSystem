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
            var created = await _service.CreateAccountAsync(account);
            if (created == null)
                return BadRequest(new { Message = "Account could not be created" });

            return Ok(created); // return full object with generated ACCxxx ID
        }

        // Partial update (PATCH)
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] Account partialAccount)
        {
            var updated = await _service.PatchAsync(id, partialAccount);
            if (updated == null)
                return NotFound(new { Message = $"Account {id} not found" });

            return Ok(updated);
        }

        // Get account by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var account = await _service.GetAccountByIdAsync(id);
            if (account == null)
                return NotFound(new { Message = $"Account {id} not found" });

            return Ok(account);
        }

        // Get accounts by Customer ID (foreign key)
        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(long customerId)
        {
            var accounts = await _service.GetAccountsByCustomerIdAsync(customerId);
            if (accounts == null || !accounts.Any())
                return NotFound(new { Message = $"No accounts found for CustomerId {customerId}" });

            return Ok(accounts);
        }

        // Full update (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Account account)
        {
            account.AccountId = id;
            var updated = await _service.UpdateAccountAsync(account);
            if (updated == null)
                return NotFound(new { Message = $"Account {id} not found" });

            return Ok(updated);
        }

        //// Delete account
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var deleted = await _service.DeleteAccountAsync(id);
        //    if (!deleted)
        //        return NotFound(new { Message = $"Account {id} not found" });

        //    return Ok(new { Message = $"Account {id} deleted successfully" });
        //}
    }
}
