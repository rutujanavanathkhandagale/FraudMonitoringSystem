using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.DTOs.AlertCase;
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

		public async Task<IEnumerable<object>> GetAllAlerts()
		{
			var alerts = await _context.Alerts
				.Where(a => a.Status != "Resolved") // 🔥 EXCLUDE DELETED ALERTS
				.Select(a => new
				{
					a.AlertID,
					a.TransactionID,
					a.Severity,
					a.Status,
					a.ReasonDetails,
					a.CreatedDate,
					HasCase = _context.Cases.Any(c => c.AlertId == a.AlertID)
				})
				.ToListAsync();

			return alerts;
		}


		public async Task<Alert> GetAlertById(int id)
    {
        if (id <= 0) throw new Exception("Invalid Alert ID");
 
        var alert = await _alertRepository.GetAlertById(id);
 
        if (alert == null) throw new Exception("Alert not found");
 
        return alert;
    }

		public async Task<Alert> CreateAlert(Alert alert)
		{
			if (alert == null)
				throw new Exception("Alert data is null");

			if (alert.TransactionID <= 0)
				throw new Exception("Invalid Transaction ID");

			if (string.IsNullOrWhiteSpace(alert.Severity))
				throw new Exception("Severity is required");

			if (string.IsNullOrWhiteSpace(alert.Status))
				throw new Exception("Status is required");

			// 🔥 GET TRANSACTION DETAILS
			var transaction = await _context.Transactions
				.FirstOrDefaultAsync(t => t.TransactionID == alert.TransactionID);

			if (transaction == null)
				throw new Exception("Transaction not found");

			// ✅ IMPORTANT FIX
			alert.CustomerId = transaction.CustomerId;

			// SET DATE
			alert.CreatedDate = DateTime.UtcNow;

			// SAVE ALERT
			_context.Alerts.Add(alert);
			await _context.SaveChangesAsync();

			return alert;
		}


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

		public async Task DeleteAlert(int id)
		{
			var alert = await _alertRepository.GetAlertById(id);
			if (alert == null) throw new Exception("Alert not found");

			// 🔥 delete mapping first
			var mappings = _context.AlertCaseMappings
				.Where(x => x.AlertID == id);

			_context.AlertCaseMappings.RemoveRange(mappings);

			// 🔥 delete related cases
			var caseIds = mappings.Select(x => x.CaseID).ToList();

			var cases = _context.Cases
				.Where(c => caseIds.Contains(c.CaseID));

			_context.Cases.RemoveRange(cases);

			// 🔥 delete alert
			await _alertRepository.DeleteAlert(id);

			await _context.SaveChangesAsync();
		}


		// 🔥 MAIN API METHOD
		public async Task<(bool success, string message)> GenerateAlertForTransaction(int transactionId)
		{
			var score = await _context.RiskScores
				.FirstOrDefaultAsync(x => x.TransactionID == transactionId);

			if (score == null)
				return (false, "No RiskScore found");

			// 🔥 CHECK IF ALERT EXISTS (INCLUDING DELETED CASE)
			var exists = await _context.Alerts
				.AnyAsync(a => a.TransactionID == transactionId);

			if (exists)
				return (false, "Alert already exists");

			await GenerateAlertsFromRiskScores(new List<RiskScore> { score });

			return (true, "Alert generated successfully");
		}


		public async Task<Case> CreateCaseFromAlert(int alertId)
		{
			var alert = await _context.Alerts.FindAsync(alertId);

			if (alert == null)
				throw new Exception("Alert not found");

			// ✅ CHECK IF CASE ALREADY EXISTS
			var exists = await _context.AlertCaseMappings
				.AnyAsync(x => x.AlertID == alertId);

			if (exists)
				throw new Exception("Case already exists");

			// ✅ CREATE CASE
			var newCase = new Case
			{
				CustomerId = alert.CustomerId,
				TransactionId = alert.TransactionID,
				CaseType = "Fraud",
				Priority = alert.Severity,
				Status = "Open",
				CreatedDate = DateTime.UtcNow
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

			return newCase;
		}



		// 🔥 CORE LOGIC
		public async Task GenerateAlertsFromRiskScores(List<RiskScore> scores)
		{
			foreach (var score in scores)
			{
				// ✅ PREVENT DUPLICATE ALERTS
				var exists = await _context.Alerts
					.AnyAsync(a => a.TransactionID == score.TransactionID);

				if (exists)
					continue;

				var severity = GetSeverity(score.ScoreValue);

				string cleanReason = score.ReasonsJSON?
					.Replace("\\u0027", "'")
					.Replace("\\n", "\n")
					.Replace("\\\"", "\"");

				var reasonDetails = new List<AlertReasonDto>();

				// 🔴 CRITICAL
				if (severity == "Critical")
				{
					reasonDetails.Add(new AlertReasonDto
					{
						Rule = "Very High Risk Score",
						Value = score.ScoreValue.ToString(),
						Status = "Triggered"
					});

					reasonDetails.Add(new AlertReasonDto
					{
						Rule = "Fraud Probability",
						Value = "Extremely High",
						Status = "Triggered"
					});
				}

				// 🟠 HIGH
				else if (severity == "High")
				{
					reasonDetails.Add(new AlertReasonDto
					{
						Rule = "High Risk Score",
						Value = score.ScoreValue.ToString(),
						Status = "Triggered"
					});

					reasonDetails.Add(new AlertReasonDto
					{
						Rule = "Suspicious Pattern",
						Value = "Detected",
						Status = "Triggered"
					});
				}

				// 🟡 MEDIUM
				else if (severity == "Medium")
				{
					reasonDetails.Add(new AlertReasonDto
					{
						Rule = "Moderate Risk Score",
						Value = score.ScoreValue.ToString(),
						Status = "Review Required"
					});

					reasonDetails.Add(new AlertReasonDto
					{
						Rule = "Case Created",
						Value = "Medium severity requires investigation",
						Status = "Triggered"
					});
				}

				// 🟢 LOW
				else if (severity == "Low")
				{
					reasonDetails.Add(new AlertReasonDto
					{
						Rule = "Low Risk Score",
						Value = score.ScoreValue.ToString(),
						Status = "Safe"
					});

					reasonDetails.Add(new AlertReasonDto
					{
						Rule = "No Case Created",
						Value = "Severity is Low, so no investigation needed",
						Status = "Closed"
					});
				}

				var reasonJson = Newtonsoft.Json.JsonConvert.SerializeObject(reasonDetails);

				// ✅ GET TRANSACTION
				var transaction = await _context.Transactions
					.FirstOrDefaultAsync(t => t.TransactionID == score.TransactionID);

				// ✅ CREATE ALERT
				var alert = new Alert
				{
					TransactionID = score.TransactionID,
					CustomerId = transaction != null ? transaction.CustomerId : 0,
					Severity = severity,
					Status = severity == "Low" ? "Closed" : "Open",
					ReasonDetails = reasonJson,
					CreatedDate = DateTime.UtcNow
				};

				// ✅ SAVE ALERT
				await _alertRepository.AddAlert(alert);
				await _context.SaveChangesAsync();
		
 


				// 🔥 NO CASE CREATION HERE
				if (severity == "High" || severity == "Critical" || severity == "Medium")
				{
					// Case will be created only when user clicks ➕ in UI
				}
				else if (severity == "Low")
				{
					// Optional: already handled via status = Closed
				}

			}
		}

	private bool IsAmlTransaction(Transaction transaction)
    {
        return transaction != null &&
               transaction.Amount >= 1000000 &&
               transaction.SourceType == "International";
    }
 
    private string GetSeverity(decimal scoreValue)
    {
        if (scoreValue >= 90) return "Critical";
        if (scoreValue >= 70) return "High";
        if (scoreValue >= 40) return "Medium";
        return "Low";
    }

		public Task<Case> UpdateCaseStatus(int caseId, string status)
		{
			throw new NotImplementedException();
		}

		
	}
 
}
