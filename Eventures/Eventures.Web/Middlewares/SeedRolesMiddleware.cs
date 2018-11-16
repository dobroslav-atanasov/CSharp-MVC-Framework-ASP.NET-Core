namespace Eventures.Web.Middlewares
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Eventures.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var dbContext = serviceProvider.GetService<EventuresDbContext>();

            if (!dbContext.Roles.Any())
            {
                await this.SeedRoles(userManager, roleManager);
            }

            await this.next(context);
        }

        private async Task SeedRoles(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminResult = roleManager.CreateAsync(new IdentityRole("Admin")).Result;
            if (adminResult.Succeeded && userManager.Users.Any())
            {
                var firstUser = userManager.Users.FirstOrDefault();
                await userManager.AddToRoleAsync(firstUser, "Admin");
            }

            await roleManager.CreateAsync(new IdentityRole("User"));
        }
    }
}