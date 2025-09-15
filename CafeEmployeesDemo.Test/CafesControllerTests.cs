using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using CafeEmployeesDemo.Data;
using CafeEmployeesDemo.Controllers;
using CafeEmployeesDemo.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace CafeEmployeesDemo.CafeEmployeesDemo.Tests
{
    public class CafesControllerTests

    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            // Seed data
            var cafe1 = new Cafe
            {
                Id = Guid.NewGuid(),
                Name = "Cafe One",
                Description = "First Cafe",
                Location = "Downtown",
                Logo = "logo1.png",
                Employees = new List<Employment>()
            };

            var cafe2 = new Cafe
            {
                Id = Guid.NewGuid(),
                Name = "Cafe Two",
                Description = "Second Cafe",
                Location = "Uptown",
                Logo = "logo2.png",
                Employees = new List<Employment>()
            };

            var employee1 = new Employee
            {
                Id = "UI00001",
                Name = "Alice",
                EmailAddress = "alice@example.com",
                PhoneNumber = "91234567",
                Gender = "Female"
            };

            var employee2 = new Employee
            {
                Id = "UI00002",
                Name = "Bob",
                EmailAddress = "bob@example.com",
                PhoneNumber = "91234568",
                Gender = "Male"
            };

            cafe1.Employees.Add(new Employment
            {
                Employee = employee1,
                EmployeeId = employee1.Id,
                Cafe = cafe1,
                CafeId = cafe1.Id,
                StartDate = DateTime.UtcNow.AddDays(-10)
            });

            cafe2.Employees.Add(new Employment
            {
                Employee = employee2,
                EmployeeId = employee2.Id,
                Cafe = cafe2,
                CafeId = cafe2.Id,
                StartDate = DateTime.UtcNow.AddDays(-5)
            });

            context.Cafes.AddRange(cafe1, cafe2);
            context.Employees.AddRange(employee1, employee2);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetCafes_ReturnsAllCafes_SortedByEmployeeCount()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new CafesController(context);

            // Act
            var result = await controller.GetCafes(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var cafes = Assert.IsAssignableFrom<IEnumerable<CafeDto>>(okResult.Value);

            Assert.Equal(2, cafes.Count());
            Assert.Equal("Cafe One", cafes.First().Name); // Cafe with more employees first
        }

        [Fact]
        public async Task GetCafes_FilterByLocation_ReturnsCorrectCafe()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new CafesController(context);

            // Act
            var result = await controller.GetCafes("Uptown");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var cafes = Assert.IsAssignableFrom<IEnumerable<CafeDto>>(okResult.Value);

            Assert.Single(cafes);
            Assert.Equal("Cafe Two", cafes.First().Name);
        }

        [Fact]
        public async Task GetCafes_InvalidLocation_ReturnsEmptyList()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new CafesController(context);

            // Act
            var result = await controller.GetCafes("Nowhere");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var cafes = Assert.IsAssignableFrom<IEnumerable<CafeDto>>(okResult.Value);

            Assert.Empty(cafes);
        }
    }
}
