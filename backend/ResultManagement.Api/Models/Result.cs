namespace ResultManagement.Api.Models;

public class Result
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public decimal Marks { get; set; }
    public string Grade { get; set; } = string.Empty;
    public decimal GradePoint { get; set; }

    public Student Student { get; set; } = null!;
    public Course Course { get; set; } = null!;
}