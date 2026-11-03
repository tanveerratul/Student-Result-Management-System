using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_backend.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        // শুরুতে Marks এবং Grade null থাকবে (এনরোলমেন্টের সময় রেজাল্ট হয় না)
        public double? Marks { get; set; }
        public string? Grade { get; set; }
    }
}