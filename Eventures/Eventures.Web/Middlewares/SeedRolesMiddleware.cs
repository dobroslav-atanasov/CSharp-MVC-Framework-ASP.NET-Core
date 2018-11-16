using Eventures.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Web.Middlewares
{
    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if (!roleManager.Roles.Any())
            {
                var adminRoleExists = roleManager.RoleExistsAsync("Admin").Result;
                var userRoleExists = roleManager.RoleExistsAsync("User").Result;
                if (!adminRoleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (!userRoleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }
            }

            await next(context);
        }
    }
}
