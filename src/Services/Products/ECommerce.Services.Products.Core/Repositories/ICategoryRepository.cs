using ECommerce.Services.Products.Core.Entities;

namespace ECommerce.Services.Products.Core.Repositories;

internal interface ICategoryRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<Category> GetAsync(string name);
    Task<IEnumerable<Category>> BrowseAsync();
    Task AddAsync(Category category);
}