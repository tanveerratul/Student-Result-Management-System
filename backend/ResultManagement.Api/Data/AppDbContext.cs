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

        b.Entity<Course>(e =>
        {
            e.HasIndex(x => x.CourseCode).IsUnique();
            e.Property(x => x.CourseCode).HasMaxLength(20).IsRequired();
            e.Property(x => x.Title).HasMaxLength(200).IsRequired();
            e.Property(x => x.Credit).HasPrecision(3, 1);
        });

        b.Entity<Result>(e =>
        {
            // The unique constraint we discussed: one result per student per course
            e.HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique();

            e.Property(x => x.Marks).HasPrecision(5, 2);
            e.Property(x => x.GradePoint).HasPrecision(3, 2);
            e.Property(x => x.Grade).HasMaxLength(3).IsRequired();

            e.HasOne(x => x.Student).WithMany(s => s.Results)
             .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);

            // Don't allow deleting a course that already has results
            e.HasOne(x => x.Course).WithMany(c => c.Results)
             .HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}