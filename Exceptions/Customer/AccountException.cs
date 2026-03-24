using System;

namespace FraudMonitoringSystem.Exceptions
{
    public class AccountException : Exception
    {
        public int StatusCode { get; }

        public AccountException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }

        // Factory methods for clarity
        public static AccountException NotFound(string message) =>
            new AccountException(message, 404);

        public static AccountException Validation(string message) =>
            new AccountException(message, 400);
    }
}
