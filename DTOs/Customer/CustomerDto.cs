namespace FraudMonitoringSystem.DTOs.Customer
{
    public class CustomerDto
    {
        public long CustomerId { get; set; }   // ✔ changed to string
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string CustomerType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly? DOB { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public string ProfileImagePath { get; set; }
    }
}
