using HomeService.Core.Entities;
using HomeService.Infrastructure.DataBase.Sql.Ef.EntitesConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.DataBase.Sql.Ef;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new AdminEntityConfigs());
        modelBuilder.ApplyConfiguration(new BidEntityConfigs());
        modelBuilder.ApplyConfiguration(new CategoryEntityConfigs());
        modelBuilder.ApplyConfiguration(new CityEntityConfigs());
        modelBuilder.ApplyConfiguration(new CommentEntityConfigs());
        modelBuilder.ApplyConfiguration(new CustomerEntityConfigs());
        modelBuilder.ApplyConfiguration(new ExpertEntityConfigs());
        modelBuilder.ApplyConfiguration(new RequestEntityConfigs());
        modelBuilder.ApplyConfiguration(new ServiceEntityConfigs());

        ApplicationUserEntityConfigs.SeedUsers(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Expert> Experts { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
