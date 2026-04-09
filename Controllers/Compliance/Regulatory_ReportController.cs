using Microsoft.AspNetCore.Mvc;

using FraudMonitoringSystem.Services.Interfaces;

using FraudMonitoringSystem.DTOs.ComplianceOfficer;

using FraudMonitoringSystem.Models.ComplianceOfficer;

namespace FraudMonitoringSystem.Controllers.ComplianceOfficer

{

    [ApiController]

    [Route("api/[controller]")]

    public class RegulatoryReportController : ControllerBase

    {

        private readonly IRegulatoryReportService _service;

        public RegulatoryReportController(IRegulatoryReportService service)

        {

            _service = service;

        }

        //CREATE REPORT

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateReportDto dto)

        {

            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var result = await _service.CreateAsync(dto);

            return Ok(result);

        }

        //GET ALL REPORTS

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            var reports = await _service.GetAllAsync();

            return Ok(reports);

        }

        //GET REPORT BY ID

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)

        {

            var report = await _service.GetByIdAsync(id);

            if (report == null)

                return NotFound($"Report with ID {id} not found");

            return Ok(report);

        }

        // GET REPORTS BY CASE ID

        [HttpGet("case/{caseId}")]

        public async Task<IActionResult> GetByCaseId(int caseId)

        {

            var reports = await _service.GetByCaseIdAsync(caseId);

            return Ok(reports);

        }

        // UPDATE REPORT

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] RegulatoryReportUpdateDto dto)

        {

            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);

            if (updated == null)

                return NotFound($"Report with ID {id} not found");

            return Ok(updated);

        }

        // DELETE REPORT

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)

        {

            var deleted = await _service.DeleteAsync(id);

            if (!deleted)

                return NotFound($"Report with ID {id} not found");

            return Ok("Deleted successfully");


        }
        // UPDATE REPORT BY CASE ID
        [HttpPut("case/{caseId}")]
        public async Task<IActionResult> UpdateByCaseId(int caseId, [FromBody] RegulatoryReportUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateByCaseIdAsync(caseId, dto);

            if (updated == null)
                return NotFound($"No report found for Case ID {caseId}");

            return Ok(updated);
        }

        // DELETE REPORT BY CASE ID
        [HttpDelete("case/{caseId}")]
        public async Task<IActionResult> DeleteByCaseId(int caseId)
        {
            var deleted = await _service.DeleteByCaseIdAsync(caseId);

            if (!deleted)
                return NotFound($"No report found for Case ID {caseId}");

            return Ok($"Report for Case #{caseId} deleted successfully");
        }

    }

}
