using ECommerce.Services.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Services.Returns.Infrastructure.EF.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new AggregateId(v));

        builder.HasMany(x => x.Products)
            .WithOne()
            .HasForeignKey(x => x.OrderId);

        builder.Property(x => x.Version)
            .IsConcurrencyToken();
    }
}