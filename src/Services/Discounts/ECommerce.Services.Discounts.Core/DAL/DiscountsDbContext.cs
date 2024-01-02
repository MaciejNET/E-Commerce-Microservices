using ECommerce.Services.Discounts.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Discounts.Core.DAL;

internal class DiscountsDbContext : DbContext
{
    public DiscountsDbContext(DbContextOptions<DiscountsDbContext> options) : base(options)
    {
    }

    public DbSet<DiscountCode> DiscountCodes { get; set; }
    public DbSet<ProductDiscount> ProductDiscounts { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}