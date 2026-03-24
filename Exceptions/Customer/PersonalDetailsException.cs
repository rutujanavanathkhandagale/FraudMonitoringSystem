namespace FraudMonitoringSystem.Exceptions
{
    public class PersonalDetailsException : Exception
    {
        public int StatusCode { get; }

        public PersonalDetailsException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }

        public static PersonalDetailsException NotFound(string msg) => new PersonalDetailsException(msg, 404);
        public static PersonalDetailsException Validation(string msg) => new PersonalDetailsException(msg, 400);
    }
}
