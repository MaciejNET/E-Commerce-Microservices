using ECommerce.Services.Discounts.Core.Entities;

namespace ECommerce.Services.Discounts.Core.Repositories;

internal interface IProductRepository
{
    Task<Product> GetAsync(Guid id);
    Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids);
    Task AddAsync(Product product);
    Task DeleteAsync(Product product);
}