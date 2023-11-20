using Microsoft.EntityFrameworkCore;
using api2.Models;

namespace api2.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
                .HasIndex(b => b.Email)
                .IsUnique();
    }

    public DbSet<api2.Models.Address> Address { get; set; }

    public DbSet<api2.Models.Category> Category { get; set; }

    public DbSet<api2.Models.Product> Product { get; set; }

    public DbSet<api2.Models.User> User { get; set; }

    public DbSet<api2.Models.Discount> Discount { get; set; }

    public DbSet<api2.Models.Order> Order { get; set; }

    public DbSet<api2.Models.Payment> Payment { get; set; }

    public DbSet<api2.Models.OrderItem> OrderItem { get; set; }
}