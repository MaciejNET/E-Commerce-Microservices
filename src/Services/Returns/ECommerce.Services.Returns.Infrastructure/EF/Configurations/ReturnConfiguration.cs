using ECommerce.Services.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Services.Returns.Infrastructure.EF.Configurations;

internal sealed class ReturnConfiguration : IEntityTypeConfiguration<Return>
{
    public void Configure(EntityTypeBuilder<Return> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new AggregateId(v));

        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, v => new UserId(v));

        builder.HasOne(x => x.Order)
            .WithMany();

        builder.HasOne(x => x.OrderProduct)
            .WithOne()
            .HasForeignKey<Return>(x => x.OrderProductId);

        builder.Property(x => x.Version)
            .IsConcurrencyToken();
    }
}