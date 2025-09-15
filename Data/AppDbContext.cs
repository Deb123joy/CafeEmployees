using CafeEmployeesDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeEmployeesDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<Employment> Employments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Seed Cafés ---
            // --- Seed Cafés ---
            var cafe1 = new Cafe
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Downtown Cafe",
                Description = "Trendy cafe in the city center",
                Location = "Downtown",
                Logo = "https://example.com/downtown.png"
            };

            var cafe2 = new Cafe
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Beachside Cafe",
                Description = "Relaxing cafe by the sea",
                Location = "Beach",
                Logo = "https://example.com/beach.png"
            };

            var cafe3 = new Cafe
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Campus Cafe",
                Description = "Student-friendly cafe near the university",
                Location = "Campus"
            };

            modelBuilder.Entity<Cafe>().HasData(cafe1, cafe2, cafe3);

            // --- Seed Employees ---
            var emp1 = new Employee
            {
                Id = "UI123456A",
                Name = "Alice Tan",
                EmailAddress = "alice@example.com",
                PhoneNumber = "91234567",
                Gender = "Female"
            };

            var emp2 = new Employee
            {
                Id = "UI234567B",
                Name = "Bob Lim",
                EmailAddress = "bob@example.com",
                PhoneNumber = "81234567",
                Gender = "Male"
            };

            var emp3 = new Employee
            {
                Id = "UI345678C",
                Name = "Chloe Ng",
                EmailAddress = "chloe@example.com",
                PhoneNumber = "92345678",
                Gender = "Female"
            };

            var emp4 = new Employee
            {
                Id = "UI456789D",
                Name = "David Wong",
                EmailAddress = "david@example.com",
                PhoneNumber = "83456789",
                Gender = "Male"
            };

            modelBuilder.Entity<Employee>().HasData(emp1, emp2, emp3, emp4);

            // --- Seed Employments ---
            modelBuilder.Entity<Employment>().HasData(
                new Employment
                {
                    EmploymentId = -1,
                    EmployeeId = emp1.Id,
                    CafeId = cafe1.Id,
                    StartDate = new DateTime(2024, 5, 1)  // Alice started May 1, 2024
                },
                new Employment
                {
                    EmploymentId = -2,
                    EmployeeId = emp2.Id,
                    CafeId = cafe1.Id,
                    StartDate = new DateTime(2024, 7, 1)  // Bob started July 1, 2024
                },
                new Employment
                {
                    EmploymentId = -3,
                    EmployeeId = emp3.Id,
                    CafeId = cafe2.Id,
                    StartDate = new DateTime(2024, 8, 15) // Chloe started Aug 15, 2024
                }
            // David has no employment yet
            );
        }
    }
}

    

