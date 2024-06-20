using HomeService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.DataBase.Sql.Ef.EntitesConfiguration;

public class BidEntityConfigs : IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        builder.ToTable("Bids");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).IsRequired();

        builder
            .HasOne(b => b.Request)
            .WithMany(r => r.Bids)
            .OnDelete(DeleteBehavior.NoAction);

    }
}