namespace Chushka.Common
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class SeedRoles
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var adminRoleExists = roleManager.RoleExistsAsync("Admin").Result;
            var userRoleExists = roleManager.RoleExistsAsync("User").Result;
            if (!adminRoleExists)
            {
                roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!userRoleExists)
            {
                roleManager.CreateAsync(new IdentityRole("User"));
            }
        }
    }
}