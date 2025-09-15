using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CafeEmployeesDemo.Models
{
    public class Employment
    {
        [Key]
        public int EmploymentId { get; set; }

        [Required]
        public string EmployeeId { get; set; } = default!;

        [Required]
        public Guid CafeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; } = default!;

        [ForeignKey(nameof(CafeId))]
        public Cafe Cafe { get; set; } = default!;
    }
}
