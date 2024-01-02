using ECommerce.Services.Discounts.Core.DTO;
using ECommerce.Services.Discounts.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Discounts.Api.Controllers;

[Authorize(Policy = Policy)]
internal class ProductDiscountController : BaseController
{
    private const string Policy = "discounts";
    private readonly IProductDiscountService _productDiscountService;

    public ProductDiscountController(IProductDiscountService productDiscountService)
    {
        _productDiscountService = productDiscountService;
    }

    [HttpPost]
    public async Task<ActionResult> Add(ProductDiscountDto dto)
    {
        await _productDiscountService.AddAsync(dto);
        AddResourceIdHeader(dto.Id);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Deleted(Guid id)
    {
        await _productDiscountService.DeleteAsync(id);

        return NoContent();
    }
}