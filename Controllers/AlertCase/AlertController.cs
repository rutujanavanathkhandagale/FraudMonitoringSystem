
using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs.AlertCase;
using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Models.AlertsCase;
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

		[HttpPost("generate-from-risk")]
		public async Task<IActionResult> GenerateFromRisk()
		{
			var scores = await _context.RiskScores.ToListAsync();

			await _alertService.GenerateAlertsFromRiskScores(scores);

			return Ok("Alerts and Cases generated successfully");
		}

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
					RuleID = dto.RuleID,
					Severity = dto.Severity,
					Status = dto.Status
				};

				var result = await _alertService.CreateAlert(alert);

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
				await _alertService.DeleteAlert(id);
				return Ok("Alert deleted successfully ✅");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

        }

    }



