using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Models.Investigator;
using Microsoft.EntityFrameworkCore;

public class TransactionPatternRepository : ITransactionPatternRepository
{
    private readonly WebContext _context;
    public TransactionPatternRepository(WebContext context) => _context = context;

    public async Task<PersonalDetails> GetCustomerByIdAsync(int customerId) =>
        await _context.PersonalDetails.FirstOrDefaultAsync(c => c.CustomerId == customerId);

    public async Task<List<Transaction>> GetTransactionsByCustomerIdAsync(int customerId) =>
        await _context.Transactions.Where(t => t.CustomerId == customerId)
            .OrderByDescending(t => t.Timestamp).ToListAsync();

    public async Task<int> GetMappedAlertCountAsync(int customerId) =>
        await (from map in _context.AlertCaseMappings
               join c in _context.Cases on map.CaseID equals c.CaseID
               where c.CustomerId == customerId
               select map).CountAsync();

    public async Task<Alert> GetHighestSeverityAlertAsync(int customerId) =>
        await _context.Alerts.Where(a => _context.Transactions
            .Where(t => t.CustomerId == customerId).Select(t => t.TransactionID).Contains(a.TransactionID))
            .OrderByDescending(a => a.Severity).FirstOrDefaultAsync();

    public async Task SaveRiskScoreAsync(RiskScore score)
    {
        _context.RiskScores.Add(score);
        await _context.SaveChangesAsync();
    }
}