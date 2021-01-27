using Microsoft.EntityFrameworkCore;

namespace FootballManager.Persistence
{
    public class FootballManagerDbContext : DbContext
    {
        public FootballManagerDbContext()
        {
        }

        public FootballManagerDbContext(DbContextOptions<FootballManagerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
