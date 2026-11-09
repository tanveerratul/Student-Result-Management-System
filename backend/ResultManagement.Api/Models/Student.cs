namespace ResultManagement.Api.Models;

public class Student
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Division { get; set; } = string.Empty;

    public User User { get; set; } = null!;
    public List<Result> Results { get; set; } = new();
}