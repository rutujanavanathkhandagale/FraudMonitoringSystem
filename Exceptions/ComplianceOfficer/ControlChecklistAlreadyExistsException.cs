namespace FraudMonitoringSystem.Exceptions.ComplianceOfficer
{
    public class ControlChecklistAlreadyExistsException : Exception
    {
        public ControlChecklistAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
