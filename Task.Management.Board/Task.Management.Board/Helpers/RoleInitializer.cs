using Microsoft.AspNetCore.Identity;
using Task.Management.Board.Models;

namespace Task.Management.Board.Helpers;

public static class RoleInitializer
{
    public static async System.Threading.Tasks.Task CreateRoles(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        string[] roleNames = ["CommonUser", "Manager", "Admin"];

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var adminUser = new ApplicationUser
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            FullName = "HEAD ADMIN"
        };

        string adminPassword = "Abc&123!";

        var user = await userManager.FindByEmailAsync("admin@example.com");

        if (user == null)
        {
            var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}