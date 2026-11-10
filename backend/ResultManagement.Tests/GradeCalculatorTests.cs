using ResultManagement.Api.Services;
using Xunit;

namespace ResultManagement.Tests;

public class GradeCalculatorTests
{
    // [Theory] runs the same test once for each [InlineData] row.
    // We test the edge of every band, because that is where bugs hide.
    [Theory]
    [InlineData(100.0, "A+", 4.00)]
    [InlineData(80.0, "A+", 4.00)]
    [InlineData(79.0, "A", 3.75)]
    [InlineData(79.5, "A", 3.75)]
    [InlineData(75.0, "A", 3.75)]
    [InlineData(74.0, "A-", 3.50)]
    [InlineData(70.0, "A-", 3.50)]
    [InlineData(69.0, "B+", 3.25)]
    [InlineData(65.0, "B+", 3.25)]
    [InlineData(64.0, "B", 3.00)]
    [InlineData(60.0, "B", 3.00)]
    [InlineData(59.0, "B-", 2.75)]
    [InlineData(55.0, "B-", 2.75)]
    [InlineData(54.0, "C+", 2.50)]
    [InlineData(50.0, "C+", 2.50)]
    [InlineData(49.0, "C", 2.25)]
    [InlineData(45.0, "C", 2.25)]
    [InlineData(44.0, "D", 2.00)]
    [InlineData(40.0, "D", 2.00)]
    [InlineData(39.0, "F", 0.00)]
    [InlineData(0.0, "F", 0.00)]
    public void Calculate_ReturnsCorrectGradeAndPoint(double marks, string expectedGrade, double expectedPoint)
    {
        // Arrange + Act
        var result = GradeCalculator.Calculate((decimal)marks);

        // Assert
        Assert.Equal(expectedGrade, result.Grade);
        Assert.Equal((decimal)expectedPoint, result.GradePoint);
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(100.01)]
    public void Calculate_InvalidMarks_Throws(double marks)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => GradeCalculator.Calculate((decimal)marks));
    }

    [Fact]
    public void Gpa_IsCreditWeighted()
    {
        // A+ (4.00) with 3 credits, C (2.25) with 1 credit
        // (4.00*3 + 2.25*1) / 4 = 3.5625 -> 3.56
        var courses = new[]
        {
            new CourseScore(4.00m, 3m),
            new CourseScore(2.25m, 1m)
        };

        Assert.Equal(3.56m, GradeCalculator.CalculateGpa(courses));
    }

    [Fact]
    public void Gpa_FailedCourseStillCounts()
    {
        // (4.00*3 + 0*3) / 6 = 2.00
        var courses = new[]
        {
            new CourseScore(4.00m, 3m),
            new CourseScore(0.00m, 3m)
        };

        Assert.Equal(2.00m, GradeCalculator.CalculateGpa(courses));
    }

    [Fact]
    public void Gpa_NoCourses_IsZero()
    {
        Assert.Equal(0m, GradeCalculator.CalculateGpa(Array.Empty<CourseScore>()));
    }

    [Fact]
    public void CompletedCredits_ExcludesFailedCourses()
    {
        var courses = new[]
        {
            new CourseScore(4.00m, 3m),   // passed
            new CourseScore(0.00m, 3m),   // failed (F)
            new CourseScore(2.00m, 1.5m)  // passed (D)
        };

        Assert.Equal(4.5m, GradeCalculator.CalculateCompletedCredits(courses));
    }
}