using ECommerce.Services.Discounts.Core.Entities;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Services.Discounts.Core.DAL.Configurations;

internal sealed class ProductDiscountConfiguration : IEntityTypeConfiguration<ProductDiscount>
{
    public void Configure(EntityTypeBuilder<ProductDiscount> builder)
    {
        builder.OwnsOne(x => x.NewPrice, npb =>
        {
            npb.Property(x => x.Amount)
                .HasPrecision(18, 2);

            npb.Property(x => x.Currency)
                .HasConversion(x => x.ToString(), v => (Currency) Enum.Parse(typeof(Currency), v));
        });
    }
}