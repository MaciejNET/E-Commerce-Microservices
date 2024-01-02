using ECommerce.Services.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Returns.Domain.Repositories;

public interface IOrderProductRepository
{
    Task<OrderProduct> GetAsync(EntityId id);
    Task AddAsync(OrderProduct orderProduct);
    Task AddRangeAsync(IEnumerable<OrderProduct> orderProducts);
    Task UpdateAsync(OrderProduct orderProduct);
}