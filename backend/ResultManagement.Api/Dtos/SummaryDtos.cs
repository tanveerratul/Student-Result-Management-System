using System.ComponentModel.DataAnnotations;

namespace ResultManagement.Api.Dtos;

// What the admin sends: marks for some of the student's courses
public class SaveResultsRequest
{
    public List<ResultEntry> Results { get; set; } = new();
}

public class ResultEntry
{
    public int CourseId { get; set; }

    // null means "no result yet"
    [Range(0, 100)]
    public decimal? Marks { get; set; }
}

// One course row in the summary
public record SummaryCourseDto(
    int CourseId, string CourseCode, string Title, decimal Credit,
    decimal? Marks, string? Grade, decimal? GradePoint);

// Everything the profile page needs
public record StudentSummaryDto(
    int StudentId, string StudentCode, string FullName, string Email, string Division,
    decimal Gpa, decimal CreditsEnrolled, decimal CreditsCompleted,
    int CoursesEnrolled, int ResultsPublished,
    List<SummaryCourseDto> Courses);