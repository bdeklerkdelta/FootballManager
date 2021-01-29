using Microsoft.EntityFrameworkCore;
using FootballManager.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using FootballManager.Persistence.Extensions;

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

        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FootballManagerDbContext).Assembly);
            modelBuilder.SetQueryFilterOnAllEntities<EntityBase>(x => x.DataState == Domain.Enumerations.EnumBag.DataState.Active);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default)
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(true, cancellationToken);
        }

        private void UpdateAuditEntities()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = (EntityBase)entry.Entity;
                DateTimeOffset now = DateTimeOffset.UtcNow;

                if (entry.State == EntityState.Modified)
                {
                    entity.ModifiedDate = now;
                }
            }
        }
    }
}
