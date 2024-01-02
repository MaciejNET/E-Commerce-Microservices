using ECommerce.Services.Reviews.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Reviews.Core.DAL;

internal class ReviewsDbContext : DbContext
{
    public ReviewsDbContext(DbContextOptions<ReviewsDbContext> options) : base(options)
    {
    }

    public DbSet<Review> Reviews { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}