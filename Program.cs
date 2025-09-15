using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CafeEmployeesDemo.Data;

var builder = WebApplication.CreateBuilder(args);
//// Register DbContext with InMemory database
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseInMemoryDatabase("CafeEmployeesDemoDB"));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // ✅ Needed for minimal APIs/Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cafe Employees API",
        Version = "v1",
        Description = "API for managing cafe employees"
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cafe Employees API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
