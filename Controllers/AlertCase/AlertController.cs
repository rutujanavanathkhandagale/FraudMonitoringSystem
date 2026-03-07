
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
using Microsoft.AspNetCore.Mvc;

    namespace FraudMonitoringSystem.Controllers.AlertCase

    {

        [Route("api/[controller]")]

        [ApiController]

        public class AlertController : ControllerBase

        {

            private readonly IAlertService _alertService;

            public AlertController(IAlertService alertService)

            {

                _alertService = alertService;

            }

            [HttpPost("create")]

            public async Task<IActionResult> CreateAlert(int transactionId, int ruleId, string severity)

            {

                var result = await _alertService.CreateAlertAsync(transactionId, ruleId, severity);

                return Ok(result);

            }

            [HttpGet("all")]

            public async Task<IActionResult> GetAllAlerts()

            {

                var result = await _alertService.GetAllAlertsAsync();

                return Ok(result);

            }

            [HttpPost("assign")]

            public async Task<IActionResult> AssignAlert(int alertId, int caseId)

            {

                var result = await _alertService.AssignAlertToCaseAsync(alertId, caseId);

                return Ok(result);

            }

        }

    }



