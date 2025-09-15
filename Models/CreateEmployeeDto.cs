using System.ComponentModel.DataAnnotations;

namespace CafeEmployeesDemo.Models
{
    public class CreateEmployeeDto
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = default!;

        [Required]
        [RegularExpression("^(8|9)[0-9]{7}$", ErrorMessage = "Phone number must start with 8 or 9 and have 8 digits.")]
        public string PhoneNumber { get; set; } = default!;

        [Required]
        [RegularExpression("Male|Female", ErrorMessage = "Gender must be either Male or Female.")]
        public string Gender { get; set; } = default!;

        // Optional: assign employee to a café immediately
        public Guid? CafeId { get; set; }
    }
}
