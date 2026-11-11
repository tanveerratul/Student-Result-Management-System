namespace ResultManagement.Api.Models;

public class Course
{
    public int Id { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal Credit { get; set; }

   public List<Enrollment> Enrollments { get; set; } = new();
}