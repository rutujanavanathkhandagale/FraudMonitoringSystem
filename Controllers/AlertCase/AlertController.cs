
using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs.AlertCase;
using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

    namespace FraudMonitoringSystem.Controllers.AlertCase

    {

	[ApiController]
	[Route("api/[controller]")]
	public class AlertsController : ControllerBase
	{
		private readonly IAlertService _alertService;
		private readonly WebContext _context;

		public AlertsController(IAlertService alertService, WebContext context)
		{
			_alertService = alertService;
			_context = context;
		}

		//[HttpPost("generate-from-risk")]
		//public async Task<IActionResult> GenerateFromRisk()
		//{
		//	var scores = await _context.RiskScores.ToListAsync();

		//	await _alertService.GenerateAlertsFromRiskScores(scores);

		//	return Ok("Alerts and Cases generated successfully");
		//}

		// ✅ GET ALL
		[HttpGet]
		public async Task<IActionResult> GetAllAlerts()
		{
			try
			{
				var alerts = await _alertService.GetAllAlerts();
				return Ok(alerts);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		// ✅ GET BY ID
		[HttpGet("{id}")]
		public async Task<IActionResult> GetAlert(int id)
		{
			try
			{
				var alert = await _alertService.GetAlertById(id);
				return Ok(alert);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// ✅ CREATE
		[HttpPost]
		public async Task<IActionResult> CreateAlert(AlertDTO dto)
		{
			try
			{
				var alert = new Alert
				{
					TransactionID = dto.TransactionID,
				
					Severity = dto.Severity,
					Status = dto.Status,
					ReasonDetails = Newtonsoft.Json.JsonConvert.SerializeObject(dto.ReasonDetails),
				};

				var result = await _alertService.CreateAlert(alert);

				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateCase([FromBody] CreateCaseDto dto)
		{
			var alert = await _context.Alerts.FindAsync(dto.AlertId);

			if (alert == null)
				return NotFound("Alert not found");

			// ✅ CHECK USING MAPPING (IMPORTANT)
			var exists = await _context.AlertCaseMappings
				.AnyAsync(x => x.AlertID == dto.AlertId);

			if (exists)
				return BadRequest("Case already exists");

			// ✅ CREATE CASE
			var newCase = new Case
			{
				CustomerId = alert.CustomerId,
				TransactionId = alert.TransactionID,
				CaseType = "Fraud",
				Priority = "High",
				Status = "Open",
				CreatedDate = DateTime.Now
			};

			_context.Cases.Add(newCase);
			await _context.SaveChangesAsync();

			// ✅ CREATE MAPPING
			var mapping = new AlertCaseMapping
			{
				AlertID = alert.AlertID,
				CaseID = newCase.CaseID
			};

			_context.AlertCaseMappings.Add(mapping);
			await _context.SaveChangesAsync();

			return Ok(newCase);
		}
		
		
		
		[HttpPost("{alertId}/create-case")]
		public async Task<IActionResult> CreateCaseFromAlert(int alertId)
		{
			try
			{
				var result = await _alertService.CreateCaseFromAlert(alertId);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("generate-cases")]
		public IActionResult GenerateCases()
		{
			var alerts = _context.Alerts
				.Where(a => a.Severity == "High" || a.Severity == "Critical")
				.ToList();

			foreach (var alert in alerts)
			{
				var existingCase = _context.Cases
					.FirstOrDefault(c => c.TransactionId == alert.TransactionID);

				if (existingCase == null)
				{
					var newCase = new Case
					{
						CustomerId = 1, // temp or fetch properly
						CaseType = alert.Severity == "Critical" ? "AML" : "Fraud",
 
						Priority = "High",
						Status = "Open",
						CreatedDate = DateTime.UtcNow
					};

					_context.Cases.Add(newCase);
					_context.SaveChanges();

					// 🔗 Mapping
					var mapping = new AlertCaseMapping
					{
						AlertID = alert.AlertID,
						CaseID = newCase.CaseID
					};

					_context.AlertCaseMappings.Add(mapping);
					_context.SaveChanges();
				}
			}

			return Ok("Cases generated successfully");
		}


		// ✅ UPDATE STATUS
		[HttpPut("{id}/status")]
		public async Task<IActionResult> UpdateAlertStatus(int id, string status)
		{
			try
			{
				var updatedAlert = await _alertService.UpdateAlert(id, status);
				return Ok(updatedAlert);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// ✅ DELETE
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAlert(int id)
		{
			try
			{
				await _alertService.UpdateAlert(id, "Resolved"); // ✅ CHANGE HERE
				return Ok("Alert resolved successfully");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("generate/{transactionId}")]
		public async Task<IActionResult> GenerateAlertForTransaction(int transactionId)
		{
			var result = await _alertService.GenerateAlertForTransaction(transactionId);

			return Ok(new
			{
				success = result.success,
				message = result.message
			});
		}


		private async Task GenerateAlertsFromRiskScores(List<RiskScore> riskScores)
		{
			throw new NotImplementedException();
		}
	}


}

    



