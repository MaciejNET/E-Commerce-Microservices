using ECommerce.Services.Products.Core.DTO;
using ECommerce.Services.Products.Core.Entities;
using ECommerce.Services.Products.Core.Exceptions;
using ECommerce.Services.Products.Core.Repositories;

namespace ECommerce.Services.Products.Core.Services;

internal class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDetailsDto>> BrowseAsync()
    {
        var categories = await _categoryRepository.BrowseAsync();

        return categories.Select(x => new CategoryDetailsDto
        {
            Id = x.Id,
            Name = x.Name,
            NumberOfProducts = x.Products.Count()
        });
    }

    public async Task AddAsync(CategoryDto dto)
    {
        if (await _categoryRepository.ExistsByNameAsync(dto.Name)) throw new CategoryAlreadyExistsException(dto.Name);

        dto.Id = Guid.NewGuid();
        var category = new Category
        {
            Id = dto.Id,
            Name = dto.Name
        };

        await _categoryRepository.AddAsync(category);
    }
}