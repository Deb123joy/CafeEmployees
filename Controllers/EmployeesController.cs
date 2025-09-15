using CafeEmployeesDemo.Data;
using CafeEmployeesDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeEmployeesDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }

    // ───────────────────────────────
    // GET /api/Employees/all
    [HttpGet("all")]
    public IActionResult GetAll()
    {
        try
        {
            var employees = _context.Employees.ToList();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error fetching employees", details = ex.Message });
        }
    }

    // GET /api/Employee/{id}
    [HttpGet("{id}")]
    public IActionResult GetEmployeeById(string id)
    {
        try
        {
            var employee = _context.Employees.Find(id);
            return employee == null ? NotFound() : Ok(employee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error fetching employee", details = ex.Message });
        }
    }

    // GET /api/Employees/filter?cafe=<cafe>
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesByCafe([FromQuery] string? cafe)
    {
        try
        {
            var query = _context.Employees
                .Include(e => e.Employment)
                .ThenInclude(em => em.Cafe)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(cafe))
            {
                query = query.Where(e => e.Employment != null &&
                                         e.Employment.Cafe.Name.ToLower() == cafe.ToLower());
            }

            var today = DateTime.UtcNow.Date;

            var employees = await query
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    Cafe = e.Employment != null ? e.Employment.Cafe.Name : string.Empty,
                    DaysWorked = e.Employment != null
                        ? EF.Functions.DateDiffDay(e.Employment.StartDate, today)
                        : 0
                })
                .OrderByDescending(e => e.DaysWorked)
                .ToListAsync();

            return Ok(employees);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error fetching filtered employees", details = ex.Message });
        }
    }

    // ───────────────────────────────
    // POST /api/Employees/simple-create (raw Employee)
    [HttpPost("simple-create")]
    public IActionResult CreateEmployee(Employee employee)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEmployeeById),
                new { id = employee.Id },
                employee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating employee", details = ex.Message });
        }
    }

    // POST /api/Employees/create-employee (with DTO)
    [HttpPost("create-employee")]
    public async Task<ActionResult<EmployeeDto>> CreateEmployeeDTO(CreateEmployeeDto dto)
    {
        try
        { 
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var random = Guid.NewGuid().ToString("N").Substring(0, 7).ToUpper();
        var employeeId = $"UI{random}";

        var employee = new Employee
        {
            Id = employeeId,
            Name = dto.Name,
            EmailAddress = dto.EmailAddress,
            PhoneNumber = dto.PhoneNumber,
            Gender = dto.Gender
        };

        _context.Employees.Add(employee);

        if (dto.CafeId.HasValue)
        {
            var cafe = await _context.Cafes.FindAsync(dto.CafeId.Value);
            if (cafe == null)
                return NotFound($"Cafe with id {dto.CafeId} not found.");

            var existingEmployment = await _context.Employments
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.Id);

            if (existingEmployment != null)
                return BadRequest("Employee already assigned to a café.");

            var employment = new Employment
            {
                EmployeeId = employee.Id,
                CafeId = cafe.Id,
                StartDate = DateTime.UtcNow
            };

            _context.Employments.Add(employment);
        }

        await _context.SaveChangesAsync();

        var response = new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            EmailAddress = employee.EmailAddress,
            PhoneNumber = employee.PhoneNumber,
            Cafe = dto.CafeId.HasValue
                ? (await _context.Cafes.FindAsync(dto.CafeId))?.Name ?? ""
                : "",
            DaysWorked = 0
        };

        return CreatedAtAction(nameof(GetEmployeeById),
            new { id = employee.Id },
            response);
    }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating employee", details = ex.Message });
        }
    }

    // ───────────────────────────────
    // PUT /api/Employees
    [HttpPut]
    public async Task<ActionResult<EmployeeDto>> UpdateEmployee(UpdateEmployeeDto dto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var employee = await _context.Employees
                .Include(e => e.Employment)
                .ThenInclude(em => em.Cafe)
                .FirstOrDefaultAsync(e => e.Id == dto.Id);

            if (employee == null)
                return NotFound($"Employee with id {dto.Id} not found.");

            employee.Name = dto.Name;
            employee.EmailAddress = dto.EmailAddress;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.Gender = dto.Gender;

            if (dto.CafeId.HasValue)
            {
                var cafe = await _context.Cafes.FindAsync(dto.CafeId.Value);
                if (cafe == null)
                    return NotFound($"Cafe with id {dto.CafeId} not found.");

                if (employee.Employment == null)
                {
                    employee.Employment = new Employment
                    {
                        EmployeeId = employee.Id,
                        CafeId = cafe.Id,
                        StartDate = DateTime.UtcNow
                    };
                }
                else if (employee.Employment.CafeId != dto.CafeId.Value)
                {
                    employee.Employment.CafeId = cafe.Id;
                    employee.Employment.StartDate = DateTime.UtcNow;
                }
            }
            else if (employee.Employment != null)
            {
                _context.Employments.Remove(employee.Employment);
            }

            await _context.SaveChangesAsync();

            var today = DateTime.UtcNow.Date;

            var response = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                Cafe = employee.Employment?.Cafe?.Name ?? string.Empty,
                DaysWorked = employee.Employment != null
                    ? (today - employee.Employment.StartDate.Date).Days
                    : 0
            };

            return Ok(response);

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating employee", details = ex.Message });
        }
    }

    // ───────────────────────────────
    // DELETE /api/Employees/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        try
        {
            var employee = await _context.Employees
                .Include(e => e.Employment)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound($"Employee with id {id} not found.");

            if (employee.Employment != null)
                _context.Employments.Remove(employee.Employment);

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return NoContent();
        }


        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting employee", details = ex.Message });
        }
    }
}
