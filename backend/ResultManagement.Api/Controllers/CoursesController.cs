using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResultManagement.Api.Data;
using ResultManagement.Api.Dtos;
using ResultManagement.Api.Models;

namespace ResultManagement.Api.Controllers;

[ApiController]
[Route("api/courses")]
[Authorize]   // every endpoint here needs a login...
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _db;

    public CoursesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<CourseDto>>> GetAll() =>
        await _db.Courses
            .OrderBy(c => c.CourseCode)
            .Select(c => new CourseDto(c.Id, c.CourseCode, c.Title, c.Credit))
            .ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CourseDto>> GetById(int id)
    {
        var course = await _db.Courses.FindAsync(id);
        return course is null ? NotFound() : ToDto(course);
    }

    [Authorize(Roles = Roles.Admin)]   // ...and these also need the Admin role
    [HttpPost]
    public async Task<ActionResult<CourseDto>> Create(SaveCourseRequest request)
    {
        var code = request.CourseCode.Trim().ToUpperInvariant();

        if (await _db.Courses.AnyAsync(c => c.CourseCode == code))
            return Conflict(new { message = $"Course code {code} already exists." });

        var course = new Course
        {
            CourseCode = code,
            Title = request.Title.Trim(),
            Credit = request.Credit
        };

        _db.Courses.Add(course);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = course.Id }, ToDto(course));
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<CourseDto>> Update(int id, SaveCourseRequest request)
    {
        var course = await _db.Courses.FindAsync(id);
        if (course is null) return NotFound();

        var code = request.CourseCode.Trim().ToUpperInvariant();

        // The same code is fine for this course, but not for a different one
        if (await _db.Courses.AnyAsync(c => c.CourseCode == code && c.Id != id))
            return Conflict(new { message = $"Course code {code} already exists." });

        course.CourseCode = code;
        course.Title = request.Title.Trim();
        course.Credit = request.Credit;

        await _db.SaveChangesAsync();
        return ToDto(course);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _db.Courses.FindAsync(id);
        if (course is null) return NotFound();

        if (await _db.Enrollments.AnyAsync(e => e.CourseId == id))
            return Conflict(new { message = "This course has enrollments and cannot be deleted." });

        _db.Courses.Remove(course);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private static CourseDto ToDto(Course c) => new(c.Id, c.CourseCode, c.Title, c.Credit);
}