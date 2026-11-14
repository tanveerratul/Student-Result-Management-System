using System.ComponentModel.DataAnnotations;

namespace ResultManagement.Api.Dtos;

// What we send back
public record CourseDto(int Id, string CourseCode, string Title, decimal Credit);

// What the admin sends when creating or updating
public class SaveCourseRequest
{
    [Required, StringLength(20)]
    public string CourseCode { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Range(0.5, 12)]
    public decimal Credit { get; set; }
}