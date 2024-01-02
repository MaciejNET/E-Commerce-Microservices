using ECommerce.Services.Reviews.Core.Entities;
using ECommerce.Services.Reviews.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Reviews.Core.DAL.Repositories;

internal class ReviewRepository : IReviewRepository
{
    private readonly ReviewsDbContext _context;

    public ReviewRepository(ReviewsDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsForProduct(Guid productId, string email)
    {
        return _context.Reviews.Where(x => x.ProductId == productId).AnyAsync(x => x.Email == email);
    }

    public Task<bool> ExistsForProduct(Guid productId, Guid userId)
    {
        return _context.Reviews.Where(x => x.ProductId == productId).AnyAsync(x => x.UserId == userId);
    }

    public Task<Review> GetAsync(Guid id)
    {
        return _context.Reviews.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Review>> GetForProductAsync(Guid productId)
    {
        return await _context.Reviews.Where(x => x.ProductId == productId).ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetForUserAsync(Guid userId)
    {
        return await _context.Reviews.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Review review)
    {
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Review review)
    {
        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
    }
}