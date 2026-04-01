using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FraudMonitoringSystem.DTOs.AlertCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;

namespace FraudMonitoringSystem.Repositories.Customer.Implementations.AlertsCase
{
	public class CaseRepository : ICaseRepository
	{
		private readonly WebContext _context;

		public CaseRepository(WebContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Case>> GetAllCases()
		{
			return await _context.Cases.ToListAsync();
		}

		public async Task<Case> GetCaseById(int id)
		{
			return await _context.Cases.FindAsync(id);
		}

		public async Task AddCase(Case caseEntity)
		{
			await _context.Cases.AddAsync(caseEntity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateCase(Case caseEntity)
		{
			_context.Cases.Update(caseEntity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteCase(int id)
		{
			var caseEntity = await _context.Cases.FindAsync(id);

			if (caseEntity != null)
			{
				_context.Cases.Remove(caseEntity);
				await _context.SaveChangesAsync();
			}
		}
	}
}
