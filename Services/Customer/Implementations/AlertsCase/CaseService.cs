using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Services.Customer.Implementations.AlertsCase
{
	public class CaseService : ICaseService
	{
		private readonly ICaseRepository _caseRepository;
		//private readonly object _context;
		private readonly WebContext _context;
		public CaseService(WebContext context, ICaseRepository caseRepository)
		{
			_context = context;
			_caseRepository = caseRepository;
		}

		public async Task<IEnumerable<Case>> GetAmlCases()
		{
			return await _context.Cases
				.Where(c => c.CaseType == "AML")
				.ToListAsync();
		}

		public async Task<IEnumerable<Case>> GetFraudCases()
		{
			return await _context.Cases
				.Where(c => c.CaseType == "Fraud")
				.ToListAsync();
		}


		// ✅ GET ALL
		public async Task<IEnumerable<Case>> GetAllCases()
		{
			return await _caseRepository.GetAllCases();
		}

		// ✅ GET BY ID
		public async Task<Case> GetCaseById(int id)
		{
			if (id <= 0)
				throw new Exception("Invalid Case ID ❌");

			var caseObj = await _caseRepository.GetCaseById(id);

			if (caseObj == null)
				throw new Exception("Case not found ❌");

			return caseObj;
		}

		// ✅ CREATE
		public async Task<Case> CreateCase(Case caseEntity)
		{
			if (caseEntity == null)
				throw new Exception("Case data is null ❌");

			if (caseEntity.CustomerId <= 0)
				throw new Exception("Invalid Customer ID ❌");

			if (string.IsNullOrWhiteSpace(caseEntity.CaseType))
				throw new Exception("Case Type is required ❌");

			if (string.IsNullOrWhiteSpace(caseEntity.Priority))
				throw new Exception("Priority is required ❌");

			if (string.IsNullOrWhiteSpace(caseEntity.Status))
				throw new Exception("Status is required ❌");

			caseEntity.CreatedDate = DateTime.UtcNow;

			await _caseRepository.AddCase(caseEntity);

			return caseEntity;
		}

		// ✅ UPDATE (Status only like Alerts)
		public async Task<Case> UpdateCaseStatus(int id, string status)
		{
			// ✅ CASE VALID STATUSES (FROM PDF)
			var validStatuses = new[] { "Open", "Investigating", "Resolved", "Reported" };

			if (!validStatuses.Contains(status))
				throw new Exception("Invalid case status");

			var caseObj = await _context.Cases.FindAsync(id);

			if (caseObj == null)
				throw new Exception("Case not found");

			// 🔥 Optional: avoid duplicate update
			if (caseObj.Status == status)
				throw new Exception("Status already same");

			caseObj.Status = status;

			await _context.SaveChangesAsync();

			return caseObj;
		}


		// ✅ DELETE
		public async Task DeleteCase(int id)
		{
			if (id <= 0)
				throw new Exception("Invalid Case ID ❌");

			var caseObj = await _caseRepository.GetCaseById(id);

			if (caseObj == null)
				throw new Exception("Case not found ❌");

			await _caseRepository.DeleteCase(id);
		}
	}
}
