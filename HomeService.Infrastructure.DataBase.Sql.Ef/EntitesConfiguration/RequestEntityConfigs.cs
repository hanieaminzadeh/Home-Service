using HomeService.Core.Entities;
using HomeService.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.DataBase.Sql.Ef.EntitesConfiguration;

public class RequestEntityConfigs : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable("Requests");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).IsRequired();

        builder.Property(o => o.Description).HasMaxLength(500);

        builder
            .HasMany(r => r.Bids)
            .WithOne(b => b.Request)
            .HasForeignKey(b => b.RequestId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(r => r.Service)
            .WithMany(s => s.Requests)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(r => r.Customer)
            .WithMany(s => s.Requests)
            .OnDelete(DeleteBehavior.NoAction);

    }
}