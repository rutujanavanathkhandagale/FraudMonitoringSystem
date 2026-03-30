using FraudMonitoringSystem.DTOs.Investigator;
using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.Services.Customer.Interfaces.Investigator;
using Microsoft.AspNetCore.Mvc;


namespace FraudMonitoringSystem.Controllers.Investigator
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // POST: api/transaction
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDto dto)
        {
            var result = await _transactionService.AddTransactionAsync(dto);
            return Ok(result);
        }

        // GET: api/transaction/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _transactionService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET: api/transaction
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _transactionService.GetAllAsync();
            return Ok(result);
        }

        // PUT: api/transaction
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TransactionDto dto)
        {
            var result = await _transactionService.UpdateAsync(dto);
            return Ok(result);
        }

        // DELETE: api/transaction/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _transactionService.DeleteAsync(id);
            return NoContent();
        }
    }
}

