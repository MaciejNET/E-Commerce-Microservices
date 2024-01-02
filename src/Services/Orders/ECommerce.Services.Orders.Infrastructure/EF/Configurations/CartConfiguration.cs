using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Configurations;

internal sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new AggregateId(v));

        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, v => new UserId(v));

        builder.Property(x => x.PreferredCurrency)
            .HasConversion(x => x.ToString(), v => (Currency) Enum.Parse(typeof(Currency), v));

        builder.HasIndex(x => x.UserId)
            .IsUnique();

        builder.HasMany(x => x.Items)
            .WithOne();

        builder.Property(x => x.Version)
            .IsConcurrencyToken();
    }
}