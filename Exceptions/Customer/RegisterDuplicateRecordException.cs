using System;

namespace FraudMonitoringSystem.Exceptions.Customer
{
    
    public class RegisterDuplicateRecordException : Exception
    {
        public RegisterDuplicateRecordException(string message) : base(message) { }
    }
}
