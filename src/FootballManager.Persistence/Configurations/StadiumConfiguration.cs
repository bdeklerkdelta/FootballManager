using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballManager.Persistence.Configurations
{
    public class StadiumConfiguration : IEntityTypeConfiguration<Stadium>
    {
        public void Configure(EntityTypeBuilder<Stadium> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.ModifiedDate).Property(p => p.Value);
            builder.OwnsOne(c => c.CreatedDate).Property(p => p.Value);

            builder.HasOne(c => c.Team).WithOne(m => m.Stadium).HasForeignKey<Team>(k => k.StadiumId);
        }
    }
}
