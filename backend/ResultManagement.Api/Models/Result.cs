namespace ResultManagement.Api.Models;

public class Result
{
    public int Id { get; set; }
    public int EnrollmentId { get; set; }
    public decimal Marks { get; set; }
    public string Grade { get; set; } = string.Empty;
    public decimal GradePoint { get; set; }

    public Enrollment Enrollment { get; set; } = null!;
}