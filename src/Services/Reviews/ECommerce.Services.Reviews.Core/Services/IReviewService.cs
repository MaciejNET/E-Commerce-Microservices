using ECommerce.Services.Reviews.Core.DTO;

namespace ECommerce.Services.Reviews.Core.Services;

internal interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetForProductAsync(Guid productId);
    Task<IEnumerable<ReviewDto>> GetForUserAsync(Guid userId);
    Task AddAsync(ReviewDetailsDto dto);
    Task UpdateAsync(ReviewDetailsDto dto);
    Task DeleteAsync(Guid id, Guid userId);
}