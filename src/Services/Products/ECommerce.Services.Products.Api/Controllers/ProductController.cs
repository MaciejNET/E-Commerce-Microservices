using ECommerce.Services.Products.Core.DTO;
using ECommerce.Services.Products.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Products.Api.Controllers;

[Authorize(Policy = Policy)]
internal class ProductController : BaseController
{
    private const string Policy = "products";
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<ProductDto>> Get(Guid id)
    {
        return OkOrNotFound(await _productService.GetAsync(id));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Browse(string? searchText, Guid? categoryId,
        decimal? minPrice, decimal? maxPrice)
    {
        return Ok(await _productService.BrowseAsync(searchText, categoryId, minPrice, maxPrice));
    }

    [HttpPost]
    public async Task<ActionResult> Add(ProductDetailsDto dto)
    {
        await _productService.AddAsync(dto);
        AddResourceIdHeader(dto.Id);
        return CreatedAtAction(nameof(Get), new {id = dto.Id}, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, ProductDetailsDto dto)
    {
        dto.Id = id;
        await _productService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}