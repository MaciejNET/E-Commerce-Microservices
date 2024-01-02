using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Configurations;

internal sealed class CheckoutCartConfiguration : IEntityTypeConfiguration<CheckoutCart>
{
    public void Configure(EntityTypeBuilder<CheckoutCart> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new AggregateId(v));

        builder.HasIndex(x => x.UserId)
            .IsUnique();

        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, v => new UserId(v));

        builder.OwnsOne(x => x.Shipment);

        builder.HasOne(x => x.Discount)
            .WithMany();

        builder.HasMany(x => x.Items)
            .WithOne();
    }
}