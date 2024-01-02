using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new AggregateId(v));

        builder.HasIndex(x => x.Sku)
            .IsUnique();

        builder.OwnsOne(x => x.StandardPrice, stb =>
        {
            stb.Property(x => x.Amount)
                .HasPrecision(18, 2);

            stb.Property(x => x.Currency)
                .HasConversion(x => x.ToString(), v => (Currency) Enum.Parse(typeof(Currency), v));
        });

        builder.OwnsOne(x => x.DiscountedPrice, stb =>
        {
            stb.Property(x => x.Amount)
                .HasPrecision(18, 2);

            stb.Property(x => x.Currency)
                .HasConversion(x => x.ToString(), v => (Currency) Enum.Parse(typeof(Currency), v));
        });
    }
}