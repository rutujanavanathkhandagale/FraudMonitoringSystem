namespace FraudMonitoringSystem.Exceptions.Rules
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
