# CafeEmployees
CafeEmployeesDemo
CafeEmployeesDemo is an ASP.NET Core Web API project that manages café employees, cafés, and employment relationships. It demonstrates CRUD operations, DTOs, EF Core, and API endpoints for a café management system.
________________________________________
Features
•	Manage employees: create, read, update, delete.
•	Manage cafés and employee-café assignments.
•	Track employee days worked.
•	Filter employees by café.
•	API documentation using Swagger/OpenAPI.
•	EF Core integration with SQL Server (or In-Memory DB for testing).
________________________________________
Technologies
•	.NET 8.0
•	ASP.NET Core Web API
•	Entity Framework Core
•	Microsoft SQL Server
•	Swagger / Swashbuckle
•	xUnit for unit testing
________________________________________
Project Structure
CafeEmployeesDemo/
│
├── CafeEmployeesDemo.sln
├── CafeEmployeesDemo/               # Main Web API project
│   ├── Controllers/                 # API controllers
│   ├── Data/                        # DbContext and migrations
│   ├── Models/                      # Entities and DTOs
│   ├── Program.cs                   # App startup
│   └── appsettings.json             # Configuration
│
└── CafeEmployeesDemo.Tests/         # Unit test project
    └── CafesControllerTests.cs
________________________________________
Setup and Installation
1.	Clone the repository:
git clone https://github.com/yourusername/CafeEmployeesDemo.git
cd CafeEmployeesDemo
2.	Open the solution in Visual Studio or VS Code.
3.	Install required NuGet packages:
dotnet restore
________________________________________
Database Configuration
•	The project uses AppDbContext for EF Core.
•	Connection string is located in appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=CafeEmployeesDemo;Trusted_Connection=True;TrustServerCertificate=True;"
}
•	Replace YOUR_SERVER with your SQL Server instance.
•	Ensure the database exists or enable migrations:
dotnet ef database update
________________________________________
Running the Project
Run the Web API from Visual Studio or CLI:
dotnet run --project CafeEmployeesDemo
The API will be available at:
https://localhost:7295
________________________________________
API Endpoints
Employees
Method	Endpoint	Description
GET	/api/Employees/all	Get all employees
GET	/api/Employees/{id}	Get employee by ID
GET	/api/Employees?cafe={name}	Filter employees by café
POST	/api/Employees/simple-create	Create an employee
POST	/api/Employees/create-employee	Create employee with café assignment
PUT	/api/Employees	Update employee info
DELETE	/api/Employees/{id}	Delete employee
Similar endpoints exist for Cafés in CafesController.
________________________________________
Testing
•	Unit tests are located in CafeEmployeesDemo.Tests.
•	Run tests using:
dotnet test
 
________________________________________
Swagger Documentation
Swagger UI is enabled for API exploration. Visit:
https://localhost:7295/swagger/index.html
•	Ensure controllers have unique route/method combinations.
•	OpenAPI version must be valid (openapi: 3.1.0).
________________________________________
Notes
•	For local development, TrustServerCertificate=True is used to bypass SQL Server SSL validation.
•	Replace with a valid SSL certificate in production.
________________________________________
Sample API Requests
Get All Employees
GET https://localhost:7295/api/Employees/all
 


 

Get Café Details
 


Create Employee with Café
 
Get employees with ID

 
Get Employee working in a Cafe
 
