namespace ResultManagement.Api.Services;

// A record is a small, read-only data holder (perfect for returning two values together)
public record GradeResult(string Grade, decimal GradePoint);

// One course's contribution to a GPA calculation
public record CourseScore(decimal GradePoint, decimal Credit);

public static class GradeCalculator
{
    // Converts marks (0-100) into a letter grade and grade point
    public static GradeResult Calculate(decimal marks)
    {
        if (marks < 0m || marks > 100m)
            throw new ArgumentOutOfRangeException(
                nameof(marks), "Marks must be between 0 and 100.");

        // A switch expression checks the cases from top to bottom
        // and returns the first one that matches.
        return marks switch
        {
            >= 80m => new GradeResult("A+", 4.00m),
            >= 75m => new GradeResult("A", 3.75m),
            >= 70m => new GradeResult("A-", 3.50m),
            >= 65m => new GradeResult("B+", 3.25m),
            >= 60m => new GradeResult("B", 3.00m),
            >= 55m => new GradeResult("B-", 2.75m),
            >= 50m => new GradeResult("C+", 2.50m),
            >= 45m => new GradeResult("C", 2.25m),
            >= 40m => new GradeResult("D", 2.00m),
            _ => new GradeResult("F", 0.00m)
        };
    }

    // Credit-weighted GPA: sum(gradePoint x credit) / sum(credit)
    // Failed courses (grade point 0) still count here, so they pull the GPA down.
    public static decimal CalculateGpa(IEnumerable<CourseScore> courses)
    {
        var list = courses.ToList();
        var totalCredits = list.Sum(c => c.Credit);

        if (totalCredits == 0m) return 0m;

        var weighted = list.Sum(c => c.GradePoint * c.Credit);
        return Math.Round(weighted / totalCredits, 2, MidpointRounding.AwayFromZero);
    }

    // Credits completed: only courses that were passed (grade point above 0)
    public static decimal CalculateCompletedCredits(IEnumerable<CourseScore> courses)
        => courses.Where(c => c.GradePoint > 0m).Sum(c => c.Credit);
}