namespace FraudMonitoringSystem.Exceptions.ComplianceOfficer
{
    public class RegulatoryReportAlreadyExistsException : Exception
    {
        public RegulatoryReportAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
