using LibraNet.Models.Entities;
using LibraNet.Services.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace LibraNet.Data;

public static class DbInitializer
{
     public static async Task InitializeAsync(
        LibraNetContext context, 
        IPasswordEncripter passwordEncripter, 
        IConfiguration configuration)
    {
        await context.Database.MigrateAsync();

        if (context.Users.Any())
            return;
   
        var adminEmail = configuration["Settings:Admin:Email"];
        var adminPassword = configuration["Settings:Admin:Password"];

        var salt = passwordEncripter.GenerateSalt();
        var encryptedPassword = passwordEncripter.Encrypt(adminPassword!, salt);

        var adminUser = new User
        {
            UserIdentifier = Guid.NewGuid(),
            Name = "Admin",
            Email = adminEmail!,
            Password = encryptedPassword, 
            Salt = salt,
            BirthDate = new DateOnly(2000, 1, 1),
            IsActive = true
        };

        var adminRole = context.Roles.FirstOrDefault(r => r.RoleName == "Admin");
        var roleUser = context.Roles.FirstOrDefault(r => r.RoleName == "User");

        if (adminRole == null)
        {
            adminRole = new Role { RoleIdentifier = Guid.NewGuid(), RoleName = "Admin" };
            context.Roles.Add(adminRole);
        }

        if (roleUser == null)
        {
            roleUser = new Role { RoleIdentifier = Guid.NewGuid(), RoleName = "User" };
            context.Roles.Add(roleUser);
        }

        var userRole = new UserRole
        {
            User = adminUser,
            Role = adminRole
        };

        context.Users.Add(adminUser);
        context.UserRoles.Add(userRole);

        await context.SaveChangesAsync();
    }
}
