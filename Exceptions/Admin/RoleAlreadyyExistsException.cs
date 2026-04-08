namespace FraudMonitoringSystem.Exceptions.Admin
{
    public class RoleAlreadyyExistsException : System.Exception
    {
        public RoleAlreadyyExistsException(string message) : base(message) { }
    }
}