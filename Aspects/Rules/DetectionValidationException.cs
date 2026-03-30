namespace FraudMonitoringSystem.Aspects.Rules
{
    [Serializable]
    internal class DetectionValidationException : Exception
    {
        public DetectionValidationException()
        {
        }

        public DetectionValidationException(string? message) : base(message)
        {
        }

        public DetectionValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}