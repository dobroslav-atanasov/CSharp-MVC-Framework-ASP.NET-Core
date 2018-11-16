namespace Eventures.Web.Middlewares.Extensions
{
    using Microsoft.AspNetCore.Builder;

    public static class SeedRolesMiddlewareExtensions
    {
        public static IApplicationBuilder UseSeedRoles(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedRolesMiddleware>();
        }
    }
}