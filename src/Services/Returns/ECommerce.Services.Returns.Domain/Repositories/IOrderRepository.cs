using ECommerce.Services.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Returns.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order> GetAsync(AggregateId id);
    Task<IEnumerable<Order>> BrowseAsync(UserId id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}