using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; 

namespace student_backend.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string RollNumber { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;
        
        public string Division { get; set; } = string.Empty;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}