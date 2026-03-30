using FraudMonitoringSystem.Models.Rules;
using FraudMonitoringSystem.Services.Customer.Interfaces.Rules;
using Microsoft.AspNetCore.Mvc;
using FraudMonitoringSystem.DTOs.Rules;


namespace FraudMonitoringSystem.Controllers.Rules
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetectionRuleController : ControllerBase
    {
        private readonly IDetectionRuleService _service;

        public DetectionRuleController(IDetectionRuleService service)
        {
            _service = service;
        }

        // GET all rules
        [HttpGet]
        public async Task<IActionResult> GetAllRules()
        {
            var rules = await _service.GetAllRulesAsync();
            return Ok(rules);
        }

        // GET rule by ID
        [HttpGet("{ruleId:int}")]
        public async Task<IActionResult> GetRuleById(int ruleId)
        {
            if (ruleId <= 0) return BadRequest(new { message = "Invalid Rule ID." });

            var rule = await _service.GetRuleByIdAsync(ruleId);
            if (rule == null) return NotFound(new { message = $"DetectionRule with ID {ruleId} not found." });

            return Ok(rule);
        }

        // POST create rule
        [HttpPost]
        public async Task<IActionResult> AddRule([FromBody] DetectionRuleCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (dto.ScenarioId <= 0) return BadRequest(new { message = "Invalid Scenario ID." });

            var result = await _service.AddRuleAsync(dto);
            if (result == "Failed to add DetectionRule")
                return BadRequest(new { message = "Failed to add DetectionRule." });

            return Ok(new { message = result });
        }

        // PUT update rule
        [HttpPut("{ruleId:int}")]
        public async Task<IActionResult> UpdateRule(int ruleId, [FromBody] DetectionRuleDto dto)
        {
            if (ruleId <= 0) return BadRequest(new { message = "Invalid Rule ID." });
            if (ruleId != dto.RuleId) return BadRequest(new { message = "ID mismatch." });

            var updatedRule = await _service.UpdateRuleAsync(dto);
            if (updatedRule == null) return NotFound(new { message = $"DetectionRule with ID {ruleId} not found." });

            return Ok(updatedRule);
        }

        // DELETE rule
        [HttpDelete("{ruleId:int}")]
        public async Task<IActionResult> DeleteRule(int ruleId)
        {
            if (ruleId <= 0) return BadRequest(new { message = "Invalid Rule ID." });

            var result = await _service.DeleteRuleAsync(ruleId);
            if (result == "Failed to delete DetectionRule")
                return NotFound(new { message = $"DetectionRule with ID {ruleId} not found." });

            return Ok(new { message = result });
        }

        // GET rules by scenario (active only)
        [HttpGet("scenario/{scenarioId:int}/rules")]
        public async Task<IActionResult> GetRulesByScenario(int scenarioId)
        {
            if (scenarioId <= 0) return BadRequest(new { message = "Invalid Scenario ID." });

            var rules = await _service.GetRulesByScenarioAsync(scenarioId);
            if (rules == null || rules.Count == 0)
                return NotFound(new { message = $"No active rules found for scenario {scenarioId}." });

            return Ok(rules);
        }

        // GET all rules by scenario (active + inactive)
        [HttpGet("scenario/{scenarioId:int}/all-rules")]
        public async Task<IActionResult> GetAllRulesByScenario(int scenarioId)
        {
            if (scenarioId <= 0) return BadRequest(new { message = "Invalid Scenario ID." });

            var rules = await _service.GetAllRulesByScenarioAsync(scenarioId);
            if (rules == null || rules.Count == 0)
                return NotFound(new { message = $"No rules found for scenario {scenarioId}." });

            return Ok(rules);
        }

        // GET rules by status (Active/Inactive)
        [HttpGet("byStatus/{status}")]
        public async Task<IActionResult> GetRulesByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest(new { message = "Status is required." });

            var rules = await _service.GetRulesByStatusAsync(status);
            if (rules == null || rules.Count == 0)
                return NotFound(new { message = $"No rules found with status {status}." });

            return Ok(rules);
        }

    }
}