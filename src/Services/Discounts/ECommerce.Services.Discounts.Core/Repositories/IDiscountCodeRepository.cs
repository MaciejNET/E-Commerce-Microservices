using ECommerce.Services.Discounts.Core.Entities;

namespace ECommerce.Services.Discounts.Core.Repositories;

internal interface IDiscountCodeRepository
{
    Task<bool> ExistsAsync(string code);
    Task<DiscountCode> GetAsync(Guid id);
    Task<List<DiscountCode>> GetNewlyAddedDiscountCodesAsync(DateTime currentDate);
    Task<List<DiscountCode>> GetExpiredCodesAsync(DateTime currentDate);
    Task AddAsync(DiscountCode discountCode);
    Task DeleteAsync(DiscountCode discountCode);
}