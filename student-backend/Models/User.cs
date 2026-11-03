using System.ComponentModel.DataAnnotations;

namespace student_backend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        [Required]
        public string Role { get; set; } = "Student";
    }
}