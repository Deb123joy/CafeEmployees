using System.ComponentModel.DataAnnotations;

namespace CafeEmployeesDemo.Models
{
    public class UpdateCafeDto
    {
        [Required]
        public Guid Id { get; set; }   // identify café to update

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        public string? Logo { get; set; }

        [Required]
        public string Location { get; set; } = default!;
    }
}
