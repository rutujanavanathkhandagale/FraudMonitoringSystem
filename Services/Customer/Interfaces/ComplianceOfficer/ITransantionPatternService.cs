namespace FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer
{
    public interface ITransactionPatternService
    {
        string CheckCustomerTransactionPattern(int customerId);
    }
}
