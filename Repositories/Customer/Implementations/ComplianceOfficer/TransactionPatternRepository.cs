using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.ComplianceOfficer;
using Microsoft.EntityFrameworkCore;
public class TransactionPatternRepository : ITransactionPatternRepository

{

    private readonly WebContext _context;

    public TransactionPatternRepository(WebContext context)

    {

        _context = context;

    }

    public async Task<string> CheckCustomerTransactionPattern(int customerId)

    {

        //Get transactions directly using CustomerId

        var transactions = await _context.Transaction

            .Where(t => t.CustomerId == customerId)

            .ToListAsync();

        if (!transactions.Any())

            return "No Transactions Found";

        var trustedSources = new[]

        {

            "Government","LIC","Insurance",

            "Salary","PensionFund","ProvidentFund",

            "Scholarship","LoanDisbursement"

        };

        var highRiskSources = new[]

        {

            "CryptoExchange","ForeignTransfer",

            "ShellCompany","OffshoreAccount"

        };

        //Rule 1: High Risk Source

        if (transactions.Any(t => highRiskSources.Contains(t.SourceType)))

            return "Suspicious";

        // Rule 2: Very High Amount from Non Trusted Source

        if (transactions.Any(t =>

            t.TransactionType == "Credit" &&

            t.Amount > 1000000 &&

            !trustedSources.Contains(t.SourceType)))

            return "Suspicious";

        //  Rule 3: Structuring (5 small transactions same day)

        var structuring = transactions

            .Where(t => t.Amount < 50000)

            .GroupBy(t => t.Timestamp.Date)

            .Any(g => g.Count() >= 5);

        if (structuring)

            return "Suspicious";

        return "Not Suspicious";

    }

}
