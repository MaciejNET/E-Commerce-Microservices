using ECommerce.Services.Reviews.Core.Entities;

namespace ECommerce.Services.Reviews.Core.Repositories;

internal interface IReviewRepository
{
    Task<bool> ExistsForProduct(Guid productId, string email);
    Task<bool> ExistsForProduct(Guid productId, Guid userId);
    Task<Review> GetAsync(Guid id);
    Task<IEnumerable<Review>> GetForProductAsync(Guid productId);
    Task<IEnumerable<Review>> GetForUserAsync(Guid userId);
    Task AddAsync(Review review);
    Task UpdateAsync(Review review);
    Task DeleteAsync(Review review);
}