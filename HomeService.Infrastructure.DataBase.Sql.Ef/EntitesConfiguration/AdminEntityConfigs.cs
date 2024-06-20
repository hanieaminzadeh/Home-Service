using HomeService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.DataBase.Sql.Ef.EntitesConfiguration;

public class AdminEntityConfigs : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.ToTable("Admins");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).IsRequired();

        builder.Property(a => a.FirstName).HasMaxLength(50);

        builder.Property(a => a.LastName).HasMaxLength(50);

        builder.Property(a => a.PhoneNumber).HasMaxLength(11);



        builder.HasData(new List<Admin>()
        {
            new Admin(){
                Id = 1,
                FirstName = "حانیه",
                LastName = "امین زاده",
                PhoneNumber = "0910000000",
                ProfileImgUrl = "/assets/img/admins/1.jpg",
				ApplicationUserId = 1,
            }
        });
    }
}