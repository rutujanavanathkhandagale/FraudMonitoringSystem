using FraudMonitoringSystem.Models.Rules;
using FraudMonitoringSystem.Services.Customer.Interfaces.Rules;
using Microsoft.AspNetCore.Mvc;


namespace FraudMonitoringSystem.Controllers.Rules
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScenarioController : ControllerBase
    {
        private readonly IScenarioService _service;

        public ScenarioController(IScenarioService service) => _service = service;

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetScenario(int id)
        {
            if (id <= 0) return BadRequest(new { message = "Invalid Scenario ID." });

            var scenario = await _service.GetScenarioByIdAsync(id);
            if (scenario == null) return NotFound(new { message = $"Scenario with ID {id} not found." });

            return Ok(scenario);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllScenarios() =>
            Ok(await _service.GetAllScenariosAsync());

        [HttpPost("create")]
        public async Task<IActionResult> CreateScenario([FromBody] Scenario scenario)
        {
            if (string.IsNullOrWhiteSpace(scenario.Name))
                return BadRequest(new { message = "Scenario Name cannot be empty." });

            var id = await _service.CreateScenarioAsync(scenario);
            if (id <= 0) return BadRequest(new { message = "Failed to create Scenario." });

            return Ok(new { success = true, scenarioId = id });
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateScenario(int id, [FromBody] Scenario scenario)
        {
            if (id <= 0) return BadRequest(new { message = "Invalid Scenario ID." });
            if (id != scenario.ScenarioId) return BadRequest(new { message = "Scenario ID mismatch." });

            var updated = await _service.UpdateScenarioAsync(scenario);
            if (!updated) return NotFound(new { message = $"Scenario with ID {id} not found." });

            return Ok(new { success = true, message = "Scenario updated successfully." });
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteScenario(int id)
        {
            if (id <= 0) return BadRequest(new { message = "Invalid Scenario ID." });

            var deleted = await _service.DeleteScenarioAsync(id);
            if (!deleted) return NotFound(new { message = $"Scenario with ID {id} not found." });

            return Ok(new { success = true, message = "Scenario deleted successfully." });
        }
        
    }
}