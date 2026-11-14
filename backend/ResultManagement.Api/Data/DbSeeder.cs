using Microsoft.EntityFrameworkCore;
using ResultManagement.Api.Models;

namespace ResultManagement.Api.Data;

public static class DbSeeder
{
    // Creates the first admin account if no admin exists yet
    public static async Task SeedAdminAsync(AppDbContext db, IConfiguration config)
    {
        var anyAdmin = await db.Users.AnyAsync(u => u.Role == Roles.Admin);
        if (anyAdmin) return;

        var email = config["SeedAdmin:Email"];
        var password = config["SeedAdmin:Password"];

        // Without these settings, do nothing instead of creating a weak account
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return;

        db.Users.Add(new User
        {
            Email = email.Trim().ToLowerInvariant(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = Roles.Admin
        });

        await db.SaveChangesAsync();
    }
}