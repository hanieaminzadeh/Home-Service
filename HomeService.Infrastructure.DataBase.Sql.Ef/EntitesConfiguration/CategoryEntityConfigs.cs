using HomeService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.DataBase.Sql.Ef.EntitesConfiguration;

public class CategoryEntityConfigs : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).IsRequired();

        builder.Property(c => c.Name).HasMaxLength(50);

        builder.HasMany(c => c.Services)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);


        builder
            .HasData(
            new Category { Id = 1, Name = "آموزش", ImgUrl = "assets/img/b-1.jpg" },
            new Category { Id = 2, Name = "حمل و نقل", ImgUrl = "assets/img/car/c-2.jpg" },
            new Category { Id = 3, Name = "خدمات منزل ", ImgUrl = "assets/img/listing/l-3.jpg" },
            new Category { Id = 4, Name = "تاسیسات", ImgUrl = "assets/img/c-4.jpg" },
            new Category { Id = 5, Name = "بنایی و ساخت و ساز", ImgUrl = "assets/img/real/r-8.jpg" },
            new Category { Id = 6, Name = "خدمات زیبایی", ImgUrl = "assets/img/listing/1-5.jpg" },
            new Category { Id = 7, Name = "سلامت و بهداشت", ImgUrl = "assets/img/med/4.jpg" },
            new Category { Id = 8, Name = "حیوانات", ImgUrl = "assets/img/med/8.jpg" },
            new Category { Id = 9, Name = "کسب و کار", ImgUrl = "assets/img/b-5.jpg" },
            new Category { Id = 10, Name = "دیجیتال", ImgUrl = "assets/img/b-4.jpg" }
            );
    }
}
