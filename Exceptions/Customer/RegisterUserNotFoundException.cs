using System;

namespace FraudMonitoringSystem.Exceptions
{
   
    public class RegisterUserNotFoundException : Exception
    {
        public RegisterUserNotFoundException(string message) : base(message) { }
    }
}
