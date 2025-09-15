using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeEmployeesDemo.Migrations
{
    /// <inheritdoc />
    public partial class SeedStaticData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("bb642fe9-b3f5-4722-82be-9f10d414dc45"));

            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("cdcbc23f-621d-45dc-b15d-e060a44f61b7"));

            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("f4357d80-bf69-4440-a435-60852273f941"));

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Description", "Location", "Logo", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Trendy cafe in the city center", "Downtown", "https://example.com/downtown.png", "Downtown Cafe" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Relaxing cafe by the sea", "Beach", "https://example.com/beach.png", "Beachside Cafe" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Student-friendly cafe near the university", "Campus", null, "Campus Cafe" }
                });

            migrationBuilder.UpdateData(
                table: "Employments",
                keyColumn: "EmploymentId",
                keyValue: -3,
                columns: new[] { "CafeId", "StartDate" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Employments",
                keyColumn: "EmploymentId",
                keyValue: -2,
                columns: new[] { "CafeId", "StartDate" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Employments",
                keyColumn: "EmploymentId",
                keyValue: -1,
                columns: new[] { "CafeId", "StartDate" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Description", "Location", "Logo", "Name" },
                values: new object[,]
                {
                    { new Guid("bb642fe9-b3f5-4722-82be-9f10d414dc45"), "Relaxing cafe by the sea", "Beach", "https://example.com/beach.png", "Beachside Cafe" },
                    { new Guid("cdcbc23f-621d-45dc-b15d-e060a44f61b7"), "Trendy cafe in the city center", "Downtown", "https://example.com/downtown.png", "Downtown Cafe" },
                    { new Guid("f4357d80-bf69-4440-a435-60852273f941"), "Student-friendly cafe near the university", "Campus", null, "Campus Cafe" }
                });

            migrationBuilder.UpdateData(
                table: "Employments",
                keyColumn: "EmploymentId",
                keyValue: -3,
                columns: new[] { "CafeId", "StartDate" },
                values: new object[] { new Guid("bb642fe9-b3f5-4722-82be-9f10d414dc45"), new DateTime(2025, 8, 11, 13, 51, 17, 231, DateTimeKind.Utc).AddTicks(2249) });

            migrationBuilder.UpdateData(
                table: "Employments",
                keyColumn: "EmploymentId",
                keyValue: -2,
                columns: new[] { "CafeId", "StartDate" },
                values: new object[] { new Guid("cdcbc23f-621d-45dc-b15d-e060a44f61b7"), new DateTime(2025, 7, 12, 13, 51, 17, 231, DateTimeKind.Utc).AddTicks(2243) });

            migrationBuilder.UpdateData(
                table: "Employments",
                keyColumn: "EmploymentId",
                keyValue: -1,
                columns: new[] { "CafeId", "StartDate" },
                values: new object[] { new Guid("cdcbc23f-621d-45dc-b15d-e060a44f61b7"), new DateTime(2025, 5, 13, 13, 51, 17, 231, DateTimeKind.Utc).AddTicks(1886) });
        }
    }
}
