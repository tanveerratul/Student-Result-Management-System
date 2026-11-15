using System.ComponentModel.DataAnnotations;

namespace ResultManagement.Api.Dtos;

// What the admin sends from the Add Student form
public class CreateStudentRequest
{
    [Required, StringLength(30)]
    public string StudentCode { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [StringLength(300)]
    public string Address { get; set; } = string.Empty;

    [Required]
    public string Division { get; set; } = string.Empty;

    // Courses the student is enrolled in (marks come later)
    public List<int> CourseIds { get; set; } = new();
}

// One row of the student table on the dashboard
public record StudentListItemDto(
    int Id, string StudentCode, string FullName, string Email,
    string Division, string Phone, int CourseCount);

// A full student with enrolled courses
public record StudentDetailDto(
    int Id, string StudentCode, string FullName, string Email,
    string Phone, string Address, string Division, List<CourseDto> Courses);

// A page of results plus the total count
public record PagedResult<T>(List<T> Items, int Total, int Page, int PageSize);