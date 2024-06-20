using HomeService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.DataBase.Sql.Ef.EntitesConfiguration;

public class CityEntityConfigs : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).IsRequired();

        builder.Property(c => c.Name).HasMaxLength(50);

        builder
            .HasMany(c => c.Experts)
            .WithOne(e => e.City)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(c => c.Customers)
            .WithOne(c => c.City)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasData(
             new City { Id = 1, Name = "تهران" },
             new City { Id = 2, Name = "اردبیل" },
             new City { Id = 3, Name = "فارس" },
             new City { Id = 4, Name = "اصفهان" },
             new City { Id = 5, Name = "زنجان" },
             new City { Id = 6, Name = "آذربایجان شرقی" },
             new City { Id = 7, Name = "آذربایجان غربی" },
             new City { Id = 8, Name = "خوزستان" },
             new City { Id = 9, Name = "مازندران" },
             new City { Id = 10, Name = "کرمان" },
             new City { Id = 11, Name = "سیستان و بلوچستان" },
             new City { Id = 12, Name = "البرز" },
             new City { Id = 13, Name = "گیلان" },
             new City { Id = 14, Name = "کرمانشاه" },
             new City { Id = 15, Name = "گلستان" },
             new City { Id = 16, Name = "لرستان" },
             new City { Id = 17, Name = "هرمزگان" },
             new City { Id = 18, Name = "همدان" },
             new City { Id = 19, Name = "کردستان" },
             new City { Id = 20, Name = "قم" },
             new City { Id = 21, Name = "مرکزی" },
             new City { Id = 22, Name = "قزوین" },
             new City { Id = 23, Name = "خراسان رضوی" },
             new City { Id = 24, Name = "یزد" },
             new City { Id = 25, Name = "بوشهر" },
             new City { Id = 26, Name = "چهارمحال بختیاری" },
             new City { Id = 27, Name = "خراسان شمالی" },
             new City { Id = 28, Name = "خراسان جنوبی" },
             new City { Id = 29, Name = "سمنان" },
             new City { Id = 30, Name = "ایلام" },
             new City { Id = 31, Name = "کهکیلویه و بویر احمد" }
             );
    }
}
