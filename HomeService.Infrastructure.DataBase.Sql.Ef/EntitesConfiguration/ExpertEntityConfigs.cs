using HomeService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.DataBase.Sql.Ef.EntitesConfiguration;

public class ExpertEntityConfigs : IEntityTypeConfiguration<Expert>
{
    public void Configure(EntityTypeBuilder<Expert> builder)
    {
        builder.ToTable("Experts");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).IsRequired();

        builder.Property(e => e.FirstName).HasMaxLength(50);

        builder.Property(e => e.LastName).HasMaxLength(50);

        builder.Property(e => e.PhoneNumber).HasMaxLength(11);

        builder.Property(e => e.CardNumber).HasMaxLength(16);

        builder.Property(e => e.ShebaNumber).HasMaxLength(26);

        builder
            .HasMany(e => e.Bids)
            .WithOne(b => b.Expert)
            .HasForeignKey(b => b.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(e => e.Services)
            .WithMany(s => s.Experts);

        builder
            .HasMany(e => e.Comments)
            .WithOne(c => c.Expert)
            .HasForeignKey(e => e.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);


        builder
            .HasData(
            new Expert
            {
                Id = 1,
                FirstName = "سارا",
                LastName = "امینی",
                Address = "تهران، اکباتان",
                CityId = 1,
				PhoneNumber = "09377907920",
				ApplicationUserId = 5
            },
            new Expert
            {
                Id = 2,
                FirstName = "میلاد",
                LastName = "امیری",
                Address = "تهران",
                CityId = 1,
				PhoneNumber = "09377907920",
				ApplicationUserId = 6
            });
    }
}
