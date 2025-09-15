namespace CafeEmployeesDemo.Models
{
    public class EmployeeDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public int DaysWorked { get; set; }
        public string Cafe { get; set; } = string.Empty;
    }
}
