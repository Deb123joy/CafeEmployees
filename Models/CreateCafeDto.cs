using System.ComponentModel.DataAnnotations;

namespace CafeEmployeesDemo.Models
{
    public class CreateCafeDto
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        public string? Logo { get; set; }   // optional

        [Required]
        public string Location { get; set; } = default!;
    }
}
