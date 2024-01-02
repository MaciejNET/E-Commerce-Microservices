using ECommerce.Services.Discounts.Core.DTO;

namespace ECommerce.Services.Discounts.Core.Services;

internal interface IDiscountCodeService
{
    Task AddAsync(DiscountCodeDto dto);
    Task DeleteAsync(Guid id);
}