using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.AlertsCase;
using Microsoft.AspNetCore.Mvc;
using FraudMonitoringSystem.DTOs.AlertCase;

namespace FraudMonitoringSystem.Controllers.AlertCase
{
	[ApiController]
	[Route("api/[controller]")]
	public class AlertCaseMappingsController : ControllerBase
	{
		private readonly WebContext _context;

		public AlertCaseMappingsController(WebContext context)
		{
			_context = context;
		}

		[HttpGet("case-alert-ids")]
		public IActionResult GetCaseWithAlertIds()
		{
			var data = _context.Cases
				.Select(c => new
				{
					caseID = c.CaseID,
					alertIDs = c.AlertCaseMappings
						.Select(ac => ac.AlertID)
						.ToList()
				})
				.ToList();

			return Ok(data);
		}
		[HttpGet("case-alert-ids/{caseId}")]
		public IActionResult GetAlertIdsByCase(int caseId)
		{
			var alertIds = _context.AlertCaseMappings
				.Where(x => x.CaseID == caseId)
				.Select(x => x.AlertID)
				.ToList();

			return Ok(alertIds);
		}


		[HttpPost]
		public IActionResult CreateMapping(AlertCaseMappingDTO dto)
		{
			var mapping = new AlertCaseMapping
			{
				AlertID = dto.AlertID,
				CaseID = dto.CaseID
			};

			_context.AlertCaseMappings.Add(mapping);
			_context.SaveChanges();

			return Ok(mapping);

		}
	}
}

