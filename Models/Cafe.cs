using System.ComponentModel.DataAnnotations;

namespace CafeEmployeesDemo.Models
{
    public class Cafe
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        public string? Logo { get; set; }   // store URL or base64

        [Required]
        public string Location { get; set; } = default!;

        public ICollection<Employment> Employees { get; set; } = new List<Employment>();
    }
}
