using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.Services.Customer.Interfaces.Investigator;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Investigator
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiskScoreController : ControllerBase
    {
        private readonly IRiskScoreService _riskScoreService;

        public RiskScoreController(IRiskScoreService riskScoreService)
        {
            _riskScoreService = riskScoreService;
        }

        // =========================================================================
        // NEW METHOD: Generate Risk Score based on Transaction ID
        // Route: POST api/riskscore/generate/5
        // =========================================================================
        [HttpPost("generate/{transactionId}")]
        public async Task<IActionResult> GenerateRiskScore(int transactionId)
        {
            var result = await _riskScoreService.GenerateRiskScoreAsync(transactionId);
            return Ok(result);
        }

        // GET: api/riskscore/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _riskScoreService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET: api/riskscore
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _riskScoreService.GetAllAsync();
            return Ok(result);
        }

        // PUT: api/riskscore
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RiskScore score)
        {
            var result = await _riskScoreService.UpdateAsync(score);
            return Ok(result);
        }

        // DELETE: api/riskscore/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _riskScoreService.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/riskscore/search/{transactionId}
        [HttpGet("search/{transactionId}")]
        public async Task<IActionResult> Search(int transactionId)
        {
            var result = await _riskScoreService.SearchAsync(transactionId);
            return Ok(result);
        }
    }
}

