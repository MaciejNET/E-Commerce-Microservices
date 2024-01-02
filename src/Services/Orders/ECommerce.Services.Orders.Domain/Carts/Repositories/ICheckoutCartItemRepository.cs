using ECommerce.Services.Orders.Domain.Carts.Entities;

namespace ECommerce.Services.Orders.Domain.Carts.Repositories;

public interface ICheckoutCartItemRepository
{
    Task AddAsync(CheckoutCartItem item);
    Task AddRangeAsync(IEnumerable<CheckoutCartItem> items);
    Task DeleteRangeAsync(IEnumerable<CheckoutCartItem> items);
}