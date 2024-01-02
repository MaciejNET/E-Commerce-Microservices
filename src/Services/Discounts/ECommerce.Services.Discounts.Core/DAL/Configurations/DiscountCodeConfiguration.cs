using ECommerce.Services.Discounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Services.Discounts.Core.DAL.Configurations;

internal sealed class DiscountCodeConfiguration : IEntityTypeConfiguration<DiscountCode>
{
    public void Configure(EntityTypeBuilder<DiscountCode> builder)
    {
        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasMany(x => x.Products)
            .WithMany(x => x.DiscountCodes)
            .UsingEntity("ProductsDiscountCodes");
    }
}