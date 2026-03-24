using System;

namespace FraudMonitoringSystem.Exceptions.Customer
{
   
    public class RegisterDatabaseException : Exception
    {
        public RegisterDatabaseException(string message) : base(message) { }
    }
}
