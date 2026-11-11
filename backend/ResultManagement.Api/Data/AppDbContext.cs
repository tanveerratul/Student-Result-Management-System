using Microsoft.EntityFrameworkCore;
using ResultManagement.Api.Models;

namespace ResultManagement.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Result> Results => Set<Result>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>(e =>
        {
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Email).HasMaxLength(200).IsRequired();
            e.Property(x => x.Role).HasMaxLength(20).IsRequired();
        });

        b.Entity<Student>(e =>
        {
            e.HasIndex(x => x.StudentCode).IsUnique();
            e.Property(x => x.StudentCode).HasMaxLength(30).IsRequired();
            e.Property(x => x.FullName).HasMaxLength(150).IsRequired();
            e.Property(x => x.Phone).HasMaxLength(20);
            e.Property(x => x.Address).HasMaxLength(300);
            e.Property(x => x.Division).HasMaxLength(30).IsRequired();

            e.HasOne(x => x.User)
             .WithOne(u => u.Student)
             .HasForeignKey<Student>(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });

       b.Entity<Enrollment>(e =>
{
    // One enrollment per student per course
    e.HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique();

    e.HasOne(x => x.Student).WithMany(s => s.Enrollments)
     .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);

    // A course that has enrollments cannot be deleted
    e.HasOne(x => x.Course).WithMany(c => c.Enrollments)
     .HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Restrict);
});

b.Entity<Result>(e =>
{
    e.Property(x => x.Marks).HasPrecision(5, 2);
    e.Property(x => x.GradePoint).HasPrecision(3, 2);
    e.Property(x => x.Grade).HasMaxLength(3).IsRequired();

    // One result per enrollment; deleted together with its enrollment
    e.HasOne(x => x.Enrollment).WithOne(en => en.Result)
     .HasForeignKey<Result>(x => x.EnrollmentId).OnDelete(DeleteBehavior.Cascade);
});
    }
}