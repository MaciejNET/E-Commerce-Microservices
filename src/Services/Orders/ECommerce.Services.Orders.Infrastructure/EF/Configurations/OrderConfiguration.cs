using ECommerce.Services.Orders.Domain.Orders.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new AggregateId(v));

        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, v => new UserId(v));

        builder.OwnsMany(x => x.Lines, lb =>
        {
            lb.WithOwner().HasForeignKey("OrderId");

            lb.Property(x => x.UnitPrice)
                .HasPrecision(18, 2);
        });

        builder.OwnsOne(x => x.Shipment);
    }
}