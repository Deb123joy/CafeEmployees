using System.ComponentModel.DataAnnotations;

namespace CafeEmployeesDemo.Models
{
    public class Employee
    {
        [Key]
        [RegularExpression(@"^UI[A-Z0-9]{7}$", ErrorMessage = "Id must be in format UIXXXXXXX")]
        public string Id { get; set; } = default!;

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = default!;

        [Required]
        [RegularExpression(@"^[89][0-9]{7}$", ErrorMessage = "Phone must start with 8 or 9 and be 8 digits")]
        public string PhoneNumber { get; set; } = default!;

        [Required]
        [RegularExpression(@"^(Male|Female)$", ErrorMessage = "Gender must be Male or Female")]
        public string Gender { get; set; } = default!;

        public Employment? Employment { get; set; }
    }
}
