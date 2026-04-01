using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs.AlertCase;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Services.Customer.Implementations.AlertsCase;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Controllers.AlertCase
{
	[ApiController]
	[Route("api/[controller]")]
	public class CaseController : ControllerBase
	{
		private readonly WebContext _context;
		private readonly ICaseService _caseService;

		public CaseController(WebContext context, ICaseService caseService)
		{
			_context = context;
			_caseService = caseService;
		}

		[HttpGet]
		public IActionResult GetCases()
		{
			var cases = _context.Cases
				.Include(c => c.AlertCaseMappings)
				.ThenInclude(m => m.Alert)
				.ToList();

			return Ok(cases);
		}

		[HttpGet("{id}")]
		public IActionResult GetCase(int id)
		{
			var caseItem = _context.Cases
				.Include(c => c.AlertCaseMappings)
				.ThenInclude(m => m.Alert)
				.FirstOrDefault(c => c.CaseID == id);

			if (caseItem == null)
				return NotFound();

			return Ok(caseItem);
		}

		[HttpPut("{id}/status")]
		public async Task<IActionResult> UpdateCaseStatus(int id, [FromQuery] string status)
		{
			var result = await _caseService.UpdateCaseStatus(id, status);
			return Ok(result);
		}

		[HttpGet("aml")]
		public async Task<IActionResult> GetAmlCases()
		{
			var result = await _caseService.GetAmlCases();
			return Ok(result);
		}

		[HttpGet("fraud")]
		public async Task<IActionResult> GetFraudCases()
		{
			var result = await _caseService.GetFraudCases();
			return Ok(result);
		}


		[HttpPost]
		public IActionResult CreateCase(CaseDTO dto)
		{
			var c = new Case
			{
				CustomerId = dto.CustomerId,
				TransactionId = dto.TransactionId,
				CaseType = string.IsNullOrEmpty(dto.CaseType) ? "Fraud" : dto.CaseType,
				Priority = string.IsNullOrEmpty(dto.Priority) ? "High" : dto.Priority,
				Status = string.IsNullOrEmpty(dto.Status) ? "Open" : dto.Status,
				CreatedDate = DateTime.UtcNow
			};

			_context.Cases.Add(c);
			_context.SaveChanges();

			return Ok(c);
		}



	}
}
