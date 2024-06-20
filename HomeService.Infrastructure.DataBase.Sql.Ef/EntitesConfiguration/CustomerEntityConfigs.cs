using HomeService.Core.Entities;
using HomeService.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.DataBase.Sql.Ef.EntitesConfiguration;

public class CustomerEntityConfigs : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).IsRequired();

        builder.Property(c => c.FirstName).HasMaxLength(50);

        builder.Property(c => c.LastName).HasMaxLength(50);

        builder.Property(c => c.PhoneNumber).HasMaxLength(11);

        builder.Property(c => c.CardNumber).HasMaxLength(16);

        builder.Property(c => c.Address).HasMaxLength(500);

        builder
            .HasMany(c => c.Requests)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasData(new List<Customer>()
            {
            new Customer()
            {
                Id = 1,
                FirstName = "علی",
                LastName = "کرامت",
                Address = "تهران، زمزم",
                PhoneNumber = "091234567",
                CardNumber = "6062731158189235",
                CityId = 1,
                ProfileImgUrl = "/assets/img/customers/1.jpg",
				ApplicationUserId = 2
            },
            new Customer()
            {
                Id = 2,
                FirstName = "علی",
                LastName = "کریمی",
                Address = "اصفهان",
                PhoneNumber = "0919048876",
                CardNumber = "2232789665980654",
                CityId = 4,
                ProfileImgUrl = "/assets/img/customers/2.jpg",
				ApplicationUserId = 3

            },
            new Customer()
            {
                Id = 3,
                FirstName = "رضا",
                LastName = "رضائی",
                Address = "قم",
                PhoneNumber = "099076483",
                CardNumber = "2345654367587790",
                CityId = 20,
                ProfileImgUrl = "/assets/img/customers/3.jpg",
				ApplicationUserId = 4

            }
            });
    }
}
