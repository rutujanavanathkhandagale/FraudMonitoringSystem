using System;

namespace FraudMonitoringSystem.Exceptions.Customer
{
   
    public class RegisterValidationException : Exception
    {
        public RegisterValidationException(string message) : base(message) { }
    }
}
