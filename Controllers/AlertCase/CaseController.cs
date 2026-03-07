using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.AlertCase
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
    {
        private readonly ICaseService _caseService;

        public CaseController(ICaseService caseService)
        {
            _caseService = caseService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCase(int primaryCustomerId, string caseType, string priority)
        {
            var result = await _caseService.CreateCaseAsync(primaryCustomerId, caseType, priority);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCases()
        {
            var result = await _caseService.GetAllCasesAsync();
            return Ok(result);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus(int caseId, string status)
        {
            var result = await _caseService.UpdateCaseStatusAsync(caseId, status);
            return Ok(result);
            
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCase(
        int caseId,string caseType, string priority)
        {
            var result = await _caseService.UpdateCaseAsync(caseId, caseType, priority);
            return Ok(result);
            }

        
    }
}
