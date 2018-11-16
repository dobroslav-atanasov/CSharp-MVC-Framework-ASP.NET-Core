namespace Eventures.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class EventuresDbContext : IdentityDbContext<User>
    {
        public EventuresDbContext(DbContextOptions<EventuresDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}