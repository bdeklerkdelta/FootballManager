using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballManager.Persistence.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.ModifiedDate).Property(p => p.Value);
            builder.OwnsOne(c => c.CreatedDate).Property(p => p.Value);

            builder.HasMany(c => c.Players).WithOne(m => m.Team).HasForeignKey(m => m.TeamId);
        }
    }
}
