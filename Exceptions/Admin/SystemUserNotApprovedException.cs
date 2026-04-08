namespace FraudMonitoringSystem.Exceptions.Admin
{

    public class SystemUserNotApprovedException : Exception
    {
        public SystemUserNotApprovedException(string message) : base(message) { }

    }

}
