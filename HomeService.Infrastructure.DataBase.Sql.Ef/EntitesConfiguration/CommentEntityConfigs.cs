using HomeService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.DataBase.Sql.Ef.EntitesConfiguration;

public class CommentEntityConfigs : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).IsRequired();

        builder.Property(c => c.CommentText).HasMaxLength(500);

        builder
            .HasOne(c => c.Expert)
            .WithMany(e => e.Comments)
            .HasForeignKey(c => c.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(c => c.Customer)
            .WithMany(c => c.Comments)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}