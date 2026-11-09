namespace ResultManagement.Api.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = Roles.Student;

    public Student? Student { get; set; }
}

public static class Roles
{
    public const string Admin = "Admin";
    public const string Student = "Student";
}