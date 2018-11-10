namespace Fdmc.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FdmcDbContext : DbContext
    {
        public FdmcDbContext()
        {
        }

        public FdmcDbContext(DbContextOptions<FdmcDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}