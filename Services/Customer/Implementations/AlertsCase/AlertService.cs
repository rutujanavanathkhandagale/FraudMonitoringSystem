using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
using Microsoft.EntityFrameworkCore;


namespace FraudMonitoringSystem.Services.Customer.Implementations.AlertsCase
{
	public class AlertService : IAlertService
	{
		private readonly IAlertRepository _alertRepository;
		private readonly WebContext _context;

		public AlertService(IAlertRepository alertRepository, WebContext context)
		{
			_alertRepository = alertRepository;
			_context = context;
		}

		// ✅ GET ALL
		public async Task<IEnumerable<Alert>> GetAllAlerts()
		{
			return await _alertRepository.GetAllAlerts();
		}

		// ✅ GET BY ID
		public async Task<Alert> GetAlertById(int id)
		{
			if (id <= 0)
				throw new Exception("Invalid Alert ID");

			var alert = await _alertRepository.GetAlertById(id);

			if (alert == null)
				throw new Exception("Alert not found");

			return alert;
		}
		//Update case status
		public async Task<Case> UpdateCaseStatus(int caseId, string status)
		{
			var validStatuses = new[] { "Open", "InReview", "Closed" };

			if (!validStatuses.Contains(status))
				throw new Exception("Invalid status");

			var caseObj = await _context.Cases.FindAsync(caseId);

			if (caseObj == null)
				throw new Exception("Case not found");

			caseObj.Status = status;

			await _context.SaveChangesAsync();

			return caseObj;
		}


		// ✅ CREATE ALERT MANUALLY
		public async Task<Alert> CreateAlert(Alert alert)
		{
			if (alert == null)
				throw new Exception("Alert data is null");

			if (alert.TransactionID <= 0)
				throw new Exception("Invalid Transaction ID");

			if (alert.RuleID <= 0)
				throw new Exception("Invalid Rule ID");

			if (string.IsNullOrWhiteSpace(alert.Severity))
				throw new Exception("Severity is required");

			if (string.IsNullOrWhiteSpace(alert.Status))
				throw new Exception("Status is required");

			alert.CreatedDate = DateTime.UtcNow;

			await _alertRepository.AddAlert(alert);
			await _context.SaveChangesAsync();
		

			return alert;
		}

		// ✅ UPDATE STATUS
		public async Task<Alert> UpdateAlert(int id, string status)
		{
			var validStatuses = new[] { "Open", "InReview", "Closed", "Escalated" };

			if (!validStatuses.Contains(status))
				throw new Exception("Invalid alert status");

			var alert = await _context.Alerts.FindAsync(id);

			if (alert == null)
				throw new Exception("Alert not found");

			alert.Status = status;

			await _context.SaveChangesAsync();

			return alert;
		}


		// ✅ DELETE
		public async Task DeleteAlert(int id)
		{
			if (id <= 0)
				throw new Exception("Invalid Alert ID");

			var alert = await _alertRepository.GetAlertById(id);

			if (alert == null)
				throw new Exception("Alert not found");

			await _alertRepository.DeleteAlert(id);
		}

		// 🔥 MAIN LOGIC: GENERATE ALERTS FROM RISKSCORE
		public async Task GenerateAlertsFromRiskScores(List<RiskScore> scores)
		{
			foreach (var score in scores)
			{
				var severity = GetSeverity(score.ScoreValue);

				var alert = new Alert
				{
					TransactionID = score.TransactionID,
					RuleID = 1,
					Severity = severity,
					Status = "Open",
					CreatedDate = DateTime.UtcNow
				};

				// ✅ Save alert
				await _alertRepository.AddAlert(alert);
				await _context.SaveChangesAsync();


				// ✅ Create case ONLY for High / Critical

				if (severity == "High" || severity == "Critical")
				{
					
					var transaction = await _context.Transactions
						.FirstOrDefaultAsync(t => t.TransactionID == score.TransactionID);

					var caseObj = new Case
					{
						TransactionId = score.TransactionID,
						CustomerId = transaction != null ? transaction.CustomerId : 0,
						CaseType = IsAmlTransaction(transaction) ? "AML" : "Fraud",
						Priority = severity,
						Status = "Open",
						CreatedDate = DateTime.UtcNow
					};

					_context.Cases.Add(caseObj);
					await _context.SaveChangesAsync();


					// ✅ Mapping
					var mapping = new AlertCaseMapping
					{
						AlertID = alert.AlertID,
						CaseID = caseObj.CaseID
					};

					_context.AlertCaseMappings.Add(mapping);
					await _context.SaveChangesAsync();
				}
			}

		}

		// AML LOGIC
		private bool IsAmlTransaction(Transaction transaction)
		{
			return transaction != null &&
				   transaction.Amount >= 1000000 &&
				   transaction.SourceType == "International";
		}


		// ✅ SEVERITY LOGIC
		private string GetSeverity(decimal scoreValue)
		{
			if (scoreValue >= 90) return "Critical";
			if (scoreValue >= 70) return "High";
			if (scoreValue >= 40) return "Medium";
			return "Low";
		}


	}
}
