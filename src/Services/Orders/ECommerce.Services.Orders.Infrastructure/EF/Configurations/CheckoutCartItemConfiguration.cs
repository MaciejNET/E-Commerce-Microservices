using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Configurations;

internal sealed class CheckoutCartItemConfiguration : IEntityTypeConfiguration<CheckoutCartItem>
{
    public void Configure(EntityTypeBuilder<CheckoutCartItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new EntityId(v));

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.OwnsOne(x => x.Price, stb =>
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