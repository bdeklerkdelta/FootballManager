using Microsoft.EntityFrameworkCore;
using FootballManager.Domain.Entities;

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

        public virtual DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FootballManagerDbContext).Assembly);
        }
    }
}
