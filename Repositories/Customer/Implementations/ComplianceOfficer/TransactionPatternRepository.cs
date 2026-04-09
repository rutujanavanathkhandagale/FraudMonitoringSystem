using FraudMonitoringSystem.Data;

using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Models.Investigator;
using Microsoft.EntityFrameworkCore;

public class TransactionPatternRepository : ITransactionPatternRepository

{

    private readonly WebContext _context;

    public TransactionPatternRepository(WebContext context)

    {

        _context = context;

    }

    // Get single transaction

    public async Task<Transaction> GetTransactionByIdAsync(int transactionId)

    {
        return await _context.Transactions

            .AsNoTracking()

            .FirstOrDefaultAsync(t => t.TransactionID == transactionId);

    }

    //Get ALL transactions of a customer

    public async Task<List<Transaction>> GetTransactionsByCustomerIdAsync(int customerId)

    {

        return await _context.Transactions

            .AsNoTracking()

            .Where(t => t.CustomerId == customerId)

            .OrderByDescending(t => t.Timestamp) // latest first

            .ToListAsync();

    }

    //Get risk score

    public async Task<RiskScore> GetRiskScoreByTransactionIdAsync(int transactionId)

    {

        return await _context.RiskScores

            .AsNoTracking()

            .FirstOrDefaultAsync(r => r.TransactionID == transactionId);

    }

}
