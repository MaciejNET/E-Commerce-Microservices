using ECommerce.Services.Products.Core.DTO;

namespace ECommerce.Services.Products.Core.Services;

internal interface ICategoryService
{
    Task<IEnumerable<CategoryDetailsDto>> BrowseAsync();
    Task AddAsync(CategoryDto dto);
}