using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResultManagement.Api.Data;
using ResultManagement.Api.Dtos;
using ResultManagement.Api.Models;

namespace ResultManagement.Api.Controllers;

[ApiController]
[Route("api/students")]
[Authorize(Roles = Roles.Admin)]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _db;

    public StudentsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<PagedResult<StudentListItemDto>>> GetAll(
        string? search, int page = 1, int pageSize = 10)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _db.Students.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(s =>
                s.FullName.ToLower().Contains(term) ||
                s.StudentCode.ToLower().Contains(term) ||
                s.User.Email.Contains(term));
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(s => s.StudentCode)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new StudentListItemDto(
                s.Id, s.StudentCode, s.FullName, s.User.Email,
                s.Division, s.Phone, s.Enrollments.Count))
            .ToListAsync();

        return new PagedResult<StudentListItemDto>(items, total, page, pageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentDetailDto>> GetById(int id)
    {
        var student = await _db.Students
            .Include(s => s.User)
            .Include(s => s.Enrollments).ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(s => s.Id == id);

        return student is null ? NotFound() : ToDetail(student);
    }

    [HttpPost]
    public async Task<ActionResult<StudentDetailDto>> Create(CreateStudentRequest request)
    {
        // The division must be one of the 8 known divisions
        var division = Divisions.All.FirstOrDefault(d =>
            string.Equals(d, request.Division.Trim(), StringComparison.OrdinalIgnoreCase));
        if (division is null)
            return BadRequest(new
            {
                message = $"Division must be one of: {string.Join(", ", Divisions.All)}."
            });

        var email = request.Email.Trim().ToLowerInvariant();
        var code = request.StudentCode.Trim().ToUpperInvariant();

        if (await _db.Users.AnyAsync(u => u.Email == email))
            return Conflict(new { message = "A user with this email already exists." });

        if (await _db.Students.AnyAsync(s => s.StudentCode == code))
            return Conflict(new { message = $"Student code {code} already exists." });

        // Every selected course must exist
        var courseIds = request.CourseIds.Distinct().ToList();
        var courses = await _db.Courses.Where(c => courseIds.Contains(c.Id)).ToListAsync();
        if (courses.Count != courseIds.Count)
            return BadRequest(new { message = "One or more selected courses do not exist." });

        var student = new Student
        {
            User = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = Roles.Student
            },
            StudentCode = code,
            FullName = request.FullName.Trim(),
            Phone = request.Phone.Trim(),
            Address = request.Address.Trim(),
            Division = division,
            Enrollments = courses.Select(c => new Enrollment { Course = c }).ToList()
        };

        _db.Students.Add(student);
        await _db.SaveChangesAsync();   // one call = one transaction

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, ToDetail(student));
    }

    private static StudentDetailDto ToDetail(Student s) => new(
        s.Id, s.StudentCode, s.FullName, s.User.Email, s.Phone, s.Address, s.Division,
        s.Enrollments
            .OrderBy(e => e.Course.CourseCode)
            .Select(e => new CourseDto(e.Course.Id, e.Course.CourseCode, e.Course.Title, e.Course.Credit))
            .ToList());
}