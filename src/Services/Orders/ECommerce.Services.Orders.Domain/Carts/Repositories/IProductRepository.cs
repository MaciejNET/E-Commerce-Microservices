using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Domain.Carts.Repositories;

public interface IProductRepository
{
    Task<Product> GetAsync(AggregateId id);
    Task<IEnumerable<Product>> BrowsAsync(IEnumerable<Guid> ids);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
}