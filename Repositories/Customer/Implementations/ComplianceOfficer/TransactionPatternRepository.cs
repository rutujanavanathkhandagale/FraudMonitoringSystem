using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.ComplianceOfficer;
using System.Linq;
using FraudMonitoringSystem.Models.Customer;


namespace FraudMonitoringSystem.Repositories.Customer.Implementations.ComplianceOfficer
{
  
    public class TransactionPatternRepository : ITransactionPatternRepository
    {
        private readonly WebContext _context;
        public TransactionPatternRepository(WebContext context)
        {
            _context = context;
        }
        public string CheckCustomerTransactionPattern(int customerId)
        {
            var transactions = (
                from t in _context.Transactions
                join a in _context.Accounts
                    on t.AccountID equals a.AccountId
                where a.CustomerId == customerId
                select t);
          
            //Trusted Sources
            var trustedSources = new[]
            {
           "Government","LIC","Insurance",
           "Salary","PensionFund","ProvidentFund",
           "Scholarship","LoanDisbursement"
       };
            // Large Trusted Credit → Not Suspicious
            if (transactions.Any(t =>
                t.TransactionType == "Credit" &&
                t.Amount >= 500000 &&
                trustedSources.Contains(t.SourceType)))
            {
                return "Not Suspicious";
            }
            // Structuring (5 small tx same day)
            var structuring = transactions
                .Where(t => t.Amount < 50000)
                .GroupBy(t => t.Timestamp.Date)
                .Any(g => g.Count() >= 5);
            if (structuring)
                return "Suspicious";
            // High Risk Sources
            var highRiskSources = new[]
            {
           "CryptoExchange","ForeignTransfer",
           "ShellCompany","OffshoreAccount"
       };
            if (transactions.Any(t =>
                highRiskSources.Contains(t.SourceType)))
            {
                return "Suspicious";
            }
            // Large Unknown Credit
            if (transactions.Any(t =>
                t.TransactionType == "Credit" &&
                t.Amount > 1000000 &&
                !trustedSources.Contains(t.SourceType)))
            {
                return "Suspicious";
            }
            return "Not Suspicious";
        }
    }
}
