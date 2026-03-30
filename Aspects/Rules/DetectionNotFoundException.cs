namespace FraudMonitoringSystem.Aspects.Rules
{
    [Serializable]
    internal class DetectionNotFoundException : Exception
    {
        public DetectionNotFoundException()
        {
        }

        public DetectionNotFoundException(string? message) : base(message)
        {
        }

        public DetectionNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}