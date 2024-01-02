using ECommerce.Services.Reviews.Core.DTO;
using ECommerce.Services.Reviews.Core.Services;
using ECommerce.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Reviews.Api.Controllers;

[Authorize(Policy = Policy)]
internal class ReviewController : BaseController
{
    private const string Policy = "reviews";
    private readonly IContext _context;
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService, IContext context)
    {
        _reviewService = reviewService;
        _context = context;
    }

    [HttpGet("product/{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetForProduct(Guid id)
    {
        return Ok(await _reviewService.GetForProductAsync(id));
    }

    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetForUser()
    {
        var id = _context.Identity.Id;

        return Ok(await _reviewService.GetForUserAsync(id));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Add(ReviewDetailsDto dto)
    {
        dto.UserId = _context.Identity.Id;
        await _reviewService.AddAsync(dto);
        AddResourceIdHeader(dto.Id);
        return CreatedAtAction(nameof(GetForProduct), new {id = dto.ProductId}, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, ReviewDetailsDto dto)
    {
        dto.Id = id;
        dto.UserId = _context.Identity.Id;
        await _reviewService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var userId = _context.Identity.Id;
        await _reviewService.DeleteAsync(id, userId);
        return NoContent();
    }
}