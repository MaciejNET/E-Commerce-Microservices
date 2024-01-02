using ECommerce.Services.Returns.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Returns.Infrastructure.EF;

internal sealed class ReturnsDbContext : DbContext
{
    public ReturnsDbContext(DbContextOptions<ReturnsDbContext> options) : base(options)
    {
    }

    public DbSet<Return> Returns { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}