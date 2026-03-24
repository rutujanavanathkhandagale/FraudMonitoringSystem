using System;

namespace FraudMonitoringSystem.Exceptions.Customer
{
    
    public class RegisterUserAlreadyExistsException : Exception
    {
        public RegisterUserAlreadyExistsException(string message) : base(message) { }
    }
}
