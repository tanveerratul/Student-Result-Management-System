using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResultManagement.Api.Data;
using ResultManagement.Api.Dtos;
using ResultManagement.Api.Models;
using ResultManagement.Api.Services;

namespace ResultManagement.Api.Controllers;

[ApiController]
[Route("api")]
public class SummaryController : ControllerBase
{
    private readonly AppDbContext _db;

    public SummaryController(AppDbContext db) => _db = db;

    // Admin: any student
    [Authorize(Roles = Roles.Admin)]
    [HttpGet("students/{id:int}/summary")]
    public async Task<ActionResult<StudentSummaryDto>> ForStudent(int id)
    {
        var student = await LoadStudent(s => s.Id == id);
        return student is null ? NotFound() : Build(student);
    }

    // Student: only their own data, found through the id inside the token
    [Authorize(Roles = Roles.Student)]
    [HttpGet("me/summary")]
    public async Task<ActionResult<StudentSummaryDto>> Mine()
    {
        var userIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdText, out var userId)) return Unauthorized();

        var student = await LoadStudent(s => s.UserId == userId);
        return student is null ? NotFound() : Build(student);
    }

    private Task<Student?> LoadStudent(Expression<Func<Student, bool>> filter) =>
        _db.Students
            .AsNoTracking()
            .Include(s => s.User)
            .Include(s => s.Enrollments).ThenInclude(e => e.Course)
            .Include(s => s.Enrollments).ThenInclude(e => e.Result)
            .FirstOrDefaultAsync(filter);

    private static StudentSummaryDto Build(Student s)
    {
        var courses = s.Enrollments
            .OrderBy(e => e.Course.CourseCode)
            .Select(e => new SummaryCourseDto(
                e.Course.Id, e.Course.CourseCode, e.Course.Title, e.Course.Credit,
                e.Result?.Marks, e.Result?.Grade, e.Result?.GradePoint))
            .ToList();

        // Only courses that have a result take part in the GPA
        var scores = s.Enrollments
            .Where(e => e.Result is not null)
            .Select(e => new CourseScore(e.Result!.GradePoint, e.Course.Credit))
            .ToList();

        return new StudentSummaryDto(
            s.Id, s.StudentCode, s.FullName, s.User.Email, s.Division,
            GradeCalculator.CalculateGpa(scores),
            courses.Sum(c => c.Credit),
            GradeCalculator.CalculateCompletedCredits(scores),
            courses.Count,
            scores.Count,
            courses);
    }
}