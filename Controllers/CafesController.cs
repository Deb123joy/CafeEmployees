using CafeEmployeesDemo.Data;
using CafeEmployeesDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CafeEmployeesDemo.Controllers;
[ApiController]
[Route("api/[controller]")]

    public class CafesController : ControllerBase
    {
    private readonly AppDbContext _context;

    public CafesController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// GET /api/cafes?location=<location>
    /// Returns cafes with employee count, sorted by employees desc
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CafeDto>>> GetCafes([FromQuery] string? location)
    {
        var query = _context.Cafes
            .Include(c => c.Employees) // load Employments
            .AsQueryable();

        // filter by location if provided
        if (!string.IsNullOrWhiteSpace(location))
        {
            query = query.Where(c => c.Location.ToLower() == location.ToLower());
        }

        var cafes = await query
            .Select(c => new CafeDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Employees = c.Employees.Count,
                Logo = c.Logo,
                Location = c.Location
            })
            .OrderByDescending(c => c.Employees)
            .ToListAsync();

        return Ok(cafes);
    }
    // POST /api/cafes
    [HttpPost]
    public async Task<ActionResult<CafeDto>> CreateCafe(CreateCafeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cafe = new Cafe
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Logo = dto.Logo,
            Location = dto.Location
        };

        _context.Cafes.Add(cafe);
        await _context.SaveChangesAsync();

        var response = new CafeDto
        {
            Id = cafe.Id,
            Name = cafe.Name,
            Description = cafe.Description,
            Employees = 0,
            Logo = cafe.Logo,
            Location = cafe.Location
        };

        return CreatedAtAction(nameof(GetCafes), new { id = cafe.Id }, response);
    }
    [HttpPut]
    public async Task<ActionResult<CafeDto>> UpdateCafe(UpdateCafeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cafe = await _context.Cafes.FindAsync(dto.Id);
        if (cafe == null)
            return NotFound($"Cafe with id {dto.Id} not found.");

        // Update fields
        cafe.Name = dto.Name;
        cafe.Description = dto.Description;
        cafe.Logo = dto.Logo;
        cafe.Location = dto.Location;

        await _context.SaveChangesAsync();

        var response = new CafeDto
        {
            Id = cafe.Id,
            Name = cafe.Name,
            Description = cafe.Description,
            Employees = await _context.Employments.CountAsync(e => e.CafeId == cafe.Id),
            Logo = cafe.Logo,
            Location = cafe.Location
        };

        return Ok(response);
    }
    // DELETE /api/cafes/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCafe(Guid id)
    {
        var cafe = await _context.Cafes
            .Include(c => c.Employees)
            .ThenInclude(e => e.Employee)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cafe == null)
            return NotFound($"Cafe with id {id} not found.");

        // Remove employees that belong to this café
        var employeeIds = cafe.Employees.Select(em => em.EmployeeId).ToList();
        var employees = await _context.Employees
            .Where(e => employeeIds.Contains(e.Id))
            .ToListAsync();

        _context.Employees.RemoveRange(employees);

        // Remove cafe (EF will handle employment records via relationships or manually if needed)
        _context.Cafes.Remove(cafe);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

