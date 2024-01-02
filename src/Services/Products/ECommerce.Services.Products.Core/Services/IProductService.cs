using ECommerce.Services.Products.Core.DTO;

namespace ECommerce.Services.Products.Core.Services;

internal interface IProductService
{
    Task<ProductDto> GetAsync(Guid id);

    Task<IEnumerable<ProductDto>> BrowseAsync(string? searchText, Guid? categoryId, decimal? minPrice,
        decimal? maxPrice);

    Task AddAsync(ProductDetailsDto dto);
    Task UpdateAsync(ProductDetailsDto dto);
    Task DeleteAsync(Guid id);
}