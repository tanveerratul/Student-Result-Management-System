using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Models; 

namespace Student_Result_Management_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<CourseResult> CourseResults { get; set; }
    }
}