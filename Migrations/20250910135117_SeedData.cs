using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeEmployeesDemo.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cafes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cafes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employments",
                columns: table => new
                {
                    EmploymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CafeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employments", x => x.EmploymentId);
                    table.ForeignKey(
                        name: "FK_Employments_Cafes_CafeId",
                        column: x => x.CafeId,
                        principalTable: "Cafes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Description", "Location", "Logo", "Name" },
                values: new object[,]
                {
                    { new Guid("bb642fe9-b3f5-4722-82be-9f10d414dc45"), "Relaxing cafe by the sea", "Beach", "https://example.com/beach.png", "Beachside Cafe" },
                    { new Guid("cdcbc23f-621d-45dc-b15d-e060a44f61b7"), "Trendy cafe in the city center", "Downtown", "https://example.com/downtown.png", "Downtown Cafe" },
                    { new Guid("f4357d80-bf69-4440-a435-60852273f941"), "Student-friendly cafe near the university", "Campus", null, "Campus Cafe" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "EmailAddress", "Gender", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { "UI123456A", "alice@example.com", "Female", "Alice Tan", "91234567" },
                    { "UI234567B", "bob@example.com", "Male", "Bob Lim", "81234567" },
                    { "UI345678C", "chloe@example.com", "Female", "Chloe Ng", "92345678" },
                    { "UI456789D", "david@example.com", "Male", "David Wong", "83456789" }
                });

            migrationBuilder.InsertData(
                table: "Employments",
                columns: new[] { "EmploymentId", "CafeId", "EmployeeId", "StartDate" },
                values: new object[,]
                {
                    { -3, new Guid("bb642fe9-b3f5-4722-82be-9f10d414dc45"), "UI345678C", new DateTime(2025, 8, 11, 13, 51, 17, 231, DateTimeKind.Utc).AddTicks(2249) },
                    { -2, new Guid("cdcbc23f-621d-45dc-b15d-e060a44f61b7"), "UI234567B", new DateTime(2025, 7, 12, 13, 51, 17, 231, DateTimeKind.Utc).AddTicks(2243) },
                    { -1, new Guid("cdcbc23f-621d-45dc-b15d-e060a44f61b7"), "UI123456A", new DateTime(2025, 5, 13, 13, 51, 17, 231, DateTimeKind.Utc).AddTicks(1886) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employments_CafeId",
                table: "Employments",
                column: "CafeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_EmployeeId",
                table: "Employments",
                column: "EmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employments");

            migrationBuilder.DropTable(
                name: "Cafes");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
