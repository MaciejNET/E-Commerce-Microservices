using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Domain.Carts.Repositories;

public interface IDiscountRepository
{
    Task<Discount> GetAsync(AggregateId id);
    Task<Discount> GetAsync(string code);
    Task AddAsync(Discount discount);
    Task DeleteAsync(Discount discount);
}