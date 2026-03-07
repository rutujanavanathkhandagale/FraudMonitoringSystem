namespace FraudMonitoringSystem.Repositories.Customer.Interfaces.ComplianceOfficer
{
    public interface ITransactionPatternRepository
    {
        string CheckCustomerTransactionPattern(int customerId);
    }
}
