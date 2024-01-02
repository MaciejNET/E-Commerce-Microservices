using ECommerce.Services.Discounts.Core.DTO;
using ECommerce.Services.Discounts.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Discounts.Api.Controllers;

[Authorize(Policy = Policy)]
internal class DiscountCodeController : BaseController
{
    private const string Policy = "discounts";
    private readonly IDiscountCodeService _discountCodeService;

    public DiscountCodeController(IDiscountCodeService discountCodeService)
    {
        _discountCodeService = discountCodeService;
    }

    [HttpPost]
    public async Task<ActionResult> Add(DiscountCodeDto dto)
    {
        await _discountCodeService.AddAsync(dto);
        AddResourceIdHeader(dto.Id);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Deleted(Guid id)
    {
        await _discountCodeService.DeleteAsync(id);

        return NoContent();
    }
}