namespace FraudMonitoringSystem.Exceptions.ComplianceOfficer
{
    public class RegulatoryReportNotFoundException : Exception
    {
        public RegulatoryReportNotFoundException(string message)
            : base(message)
        {
        }
    }
}
