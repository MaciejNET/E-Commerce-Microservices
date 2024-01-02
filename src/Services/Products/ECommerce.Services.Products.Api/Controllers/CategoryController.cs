using ECommerce.Services.Products.Core.DTO;
using ECommerce.Services.Products.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Products.Api.Controllers;

internal class CategoryController : BaseController
{
    private const string Policy = "categories";
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDetailsDto>>> Browse()
    {
        return Ok(await _categoryService.BrowseAsync());
    }

    [HttpPost]
    [Authorize(Policy = Policy)]
    public async Task<ActionResult> Add(CategoryDto dto)
    {
        await _categoryService.AddAsync(dto);
        AddResourceIdHeader(dto.Id);
        return CreatedAtAction(nameof(Browse), null, null);
    }
}