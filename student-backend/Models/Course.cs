using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace student_backend.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string CourseCode { get; set; } = string.Empty;

        [Required]
        public string CourseTitle { get; set; } = string.Empty;

        public double CreditHours { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}