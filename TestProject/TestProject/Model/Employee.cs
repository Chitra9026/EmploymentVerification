namespace TestProject.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }
        public string VerificationCode { get; set; }
    }
    public class VerificationRequest
    {
        public int EmployeeId { get; set; }
        public string CompanyName { get; set; }
        public string VerificationCode { get; set; }
    }
}

