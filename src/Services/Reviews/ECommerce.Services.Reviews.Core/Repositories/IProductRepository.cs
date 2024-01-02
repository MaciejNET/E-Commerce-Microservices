using ECommerce.Services.Reviews.Core.Entities;

namespace ECommerce.Services.Reviews.Core.Repositories;

internal interface IProductRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(Product product);
    Task DeleteAsync(Product product);
}