using ECommerce.Services.Reviews.Core.DTO;
using ECommerce.Services.Reviews.Core.Entities;
using ECommerce.Services.Reviews.Core.Exceptions;
using ECommerce.Services.Reviews.Core.Repositories;
using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Services.Reviews.Core.Services;

internal class ReviewService : IReviewService
{
    private readonly IClock _clock;
    private readonly IProductRepository _productRepository;
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository, IClock clock, IProductRepository productRepository)
    {
        _reviewRepository = reviewRepository;
        _clock = clock;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ReviewDto>> GetForProductAsync(Guid productId)
    {
        var products = await _reviewRepository.GetForProductAsync(productId);

        return products.Select(Map);
    }

    public async Task<IEnumerable<ReviewDto>> GetForUserAsync(Guid userId)
    {
        var products = await _reviewRepository.GetForUserAsync(userId);

        return products.Select(Map);
    }

    public async Task AddAsync(ReviewDetailsDto dto)
    {
        if (!await _productRepository.ExistsAsync(dto.ProductId)) throw new ProductNotFoundException(dto.ProductId);

        if (await _reviewRepository.ExistsForProduct(dto.ProductId, dto.Email))
            throw new ReviewExistsException(dto.Email);

        if (dto.UserId.HasValue && await _reviewRepository.ExistsForProduct(dto.ProductId, dto.UserId.Value))
            throw new ReviewExistsException(dto.UserId.Value);

        dto.Id = Guid.NewGuid();
        var review = new Review
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            UserId = dto.UserId,
            ProductId = dto.ProductId,
            Content = dto.Content,
            Rating = dto.Rating,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate()
        };

        await _reviewRepository.AddAsync(review);
    }

    public async Task UpdateAsync(ReviewDetailsDto dto)
    {
        var review = await _reviewRepository.GetAsync(dto.Id);

        if (review is null) throw new ReviewNotFoundException(dto.Id);

        if (dto.UserId != review.UserId) throw new ReviewUpdateAuthorizationException(dto.Id);

        review.Content = dto.Content;
        review.Rating = dto.Rating;
        review.ModifiedAt = _clock.CurrentDate();
        await _reviewRepository.UpdateAsync(review);
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var review = await _reviewRepository.GetAsync(id);

        if (review is null) throw new ReviewNotFoundException(id);

        if (review.UserId != userId) throw new ReviewDeleteAuthorizationException(id);

        await _reviewRepository.DeleteAsync(review);
    }

    private static ReviewDto Map(Review review)
    {
        return new ReviewDto
        {
            Id = review.Id,
            Name = review.Name,
            Email = review.Email,
            Content = review.Content,
            Rating = review.Rating,
            CreatedAt = review.CreatedAt,
            ModifiedAt = review.ModifiedAt
        };
    }
}