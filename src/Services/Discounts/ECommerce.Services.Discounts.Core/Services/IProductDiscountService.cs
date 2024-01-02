using ECommerce.Services.Discounts.Core.DTO;

namespace ECommerce.Services.Discounts.Core.Services;

internal interface IProductDiscountService
{
    Task AddAsync(ProductDiscountDto dto);
    Task DeleteAsync(Guid id);
}