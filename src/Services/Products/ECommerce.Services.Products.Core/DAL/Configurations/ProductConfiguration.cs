using ECommerce.Services.Products.Core.Entities;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Services.Products.Core.DAL.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(x => x.Sku)
            .IsUnique();

        builder.OwnsOne(x => x.StandardPrice, spb =>
        {
            spb.Property(x => x.Amount)
                .HasPrecision(18, 2);

            spb.Property(x => x.Currency)
                .HasConversion(x => x.ToString(), v => (Currency) Enum.Parse(typeof(Currency), v));
        });

        builder.OwnsOne(x => x.DiscountedPrice, spb =>
        {
            spb.Property(x => x.Amount)
                .HasPrecision(18, 2);

            spb.Property(x => x.Currency)
                .HasConversion(x => x.ToString(), v => (Currency) Enum.Parse(typeof(Currency), v));
        });
    }
}