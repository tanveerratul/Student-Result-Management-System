using System.Text.Json.Serialization; 

namespace Student_Result_Management_System.Models 
{
    public class CourseResult
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        [JsonIgnore] 
        public Student? Student { get; set; } 

        public string? CourseCode { get; set; }
        public string? CourseTitle { get; set; }
        public decimal Credit { get; set; }
        public decimal GradePoint { get; set; }
    }
}