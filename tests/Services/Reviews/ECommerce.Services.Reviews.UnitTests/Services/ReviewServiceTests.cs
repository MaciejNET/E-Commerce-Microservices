using ECommerce.Services.Reviews.Core.DTO;
using ECommerce.Services.Reviews.Core.Entities;
using ECommerce.Services.Reviews.Core.Exceptions;
using ECommerce.Services.Reviews.Core.Repositories;
using ECommerce.Services.Reviews.Core.Services;
using ECommerce.Services.Reviews.UnitTests.Time;
using ECommerce.Shared.Abstractions.Time;
using FluentAssertions;
using Moq;

namespace ECommerce.Services.Reviews.UnitTests.Services;

public class ReviewServiceTests
{
    private readonly IClock _clock;

    public ReviewServiceTests()
    {
        _clock = new TestClock();
    }

    [Fact]
    public async Task AddAsync_GivenUserDoesNotPlacedReview_ShouldAddReviewSuccessfully()
    {
        //Arrange
        var reviewDetailsDto = new ReviewDetailsDto
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "test@test.com",
            Content = "Test",
            Rating = 3,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate(),
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid()
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        productRepositoryMock
            .Setup(x => x.ExistsAsync(reviewDetailsDto.ProductId))
            .ReturnsAsync<IProductRepository, bool>(true);

        reviewRepositoryMock
            .Setup(x => x.ExistsForProduct(reviewDetailsDto.ProductId, reviewDetailsDto.Email))
            .ReturnsAsync<IReviewRepository, bool>(false);

        reviewRepositoryMock
            .Setup(x => x.ExistsForProduct(reviewDetailsDto.ProductId, reviewDetailsDto.UserId.Value))
            .ReturnsAsync<IReviewRepository, bool>(false);

        var reviewService = new ReviewService(reviewRepositoryMock.Object, _clock, productRepositoryMock.Object);

        //Act
        await reviewService.AddAsync(reviewDetailsDto);

        //Assert
        productRepositoryMock.Verify(x => x.ExistsAsync(reviewDetailsDto.ProductId), Times.Once);
        reviewRepositoryMock.Verify(x => x.ExistsForProduct(reviewDetailsDto.ProductId, reviewDetailsDto.Email),
            Times.Once);
        reviewRepositoryMock.Verify(x => x.ExistsForProduct(reviewDetailsDto.ProductId, reviewDetailsDto.UserId.Value),
            Times.Once);
        reviewRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Review>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_GivenProductDoesNotExists_ShouldThrowProductNotFoundException()
    {
        //Arrange
        var reviewDetailsDto = new ReviewDetailsDto
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "test@test.com",
            Content = "Test",
            Rating = 3,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate(),
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid()
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        productRepositoryMock
            .Setup(x => x.ExistsAsync(reviewDetailsDto.ProductId))
            .ReturnsAsync<IProductRepository, bool>(false);

        var reviewService = new ReviewService(reviewRepositoryMock.Object, _clock, productRepositoryMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => reviewService.AddAsync(reviewDetailsDto));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<ProductNotFoundException>();
    }

    [Fact]
    public async Task AddAsync_GivenEmailAlreadyHasReview_ShouldThrowReviewExistsException()
    {
        //Arrange
        var reviewDetailsDto = new ReviewDetailsDto
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "test@test.com",
            Content = "Test",
            Rating = 3,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate(),
            ProductId = Guid.NewGuid()
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        productRepositoryMock
            .Setup(x => x.ExistsAsync(reviewDetailsDto.ProductId))
            .ReturnsAsync<IProductRepository, bool>(true);

        reviewRepositoryMock
            .Setup(x => x.ExistsForProduct(reviewDetailsDto.ProductId, reviewDetailsDto.Email))
            .ReturnsAsync<IReviewRepository, bool>(true);

        var reviewService = new ReviewService(reviewRepositoryMock.Object, _clock, productRepositoryMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => reviewService.AddAsync(reviewDetailsDto));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<ReviewExistsException>();
    }

    [Fact]
    public async Task AddAsync_GivenUserAlreadyHasReview_ShouldThrowReviewExistsException()
    {
        //Arrange
        var reviewDetailsDto = new ReviewDetailsDto
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "test@test.com",
            Content = "Test",
            Rating = 3,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate(),
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid()
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        productRepositoryMock
            .Setup(x => x.ExistsAsync(reviewDetailsDto.ProductId))
            .ReturnsAsync<IProductRepository, bool>(true);

        reviewRepositoryMock
            .Setup(x => x.ExistsForProduct(reviewDetailsDto.ProductId, reviewDetailsDto.Email))
            .ReturnsAsync<IReviewRepository, bool>(false);

        reviewRepositoryMock
            .Setup(x => x.ExistsForProduct(reviewDetailsDto.ProductId, reviewDetailsDto.UserId.Value))
            .ReturnsAsync<IReviewRepository, bool>(true);

        var reviewService = new ReviewService(reviewRepositoryMock.Object, _clock, productRepositoryMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => reviewService.AddAsync(reviewDetailsDto));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<ReviewExistsException>();
    }

    [Fact]
    public async Task UpdateAsync_ValidReview_ShouldUpdateSuccessfully()
    {
        //Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var reviewDetailsDto = new ReviewDetailsDto
        {
            Id = reviewId,
            Name = "Test",
            Email = "test@test.com",
            Content = "Test",
            Rating = 3,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate(),
            UserId = userId,
            ProductId = productId
        };

        var existingReview = new Review
        {
            Id = reviewId,
            Name = "Test",
            Email = "test@test.com",
            Content = "Test",
            Rating = 3,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate(),
            UserId = userId,
            ProductId = productId
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        reviewRepositoryMock
            .Setup(x => x.GetAsync(reviewId))
            .ReturnsAsync(existingReview);

        var reviewService = new ReviewService(reviewRepositoryMock.Object, _clock, productRepositoryMock.Object);

        //Act
        await reviewService.UpdateAsync(reviewDetailsDto);

        //Assert
        reviewRepositoryMock.Verify(x => x.GetAsync(reviewId), Times.Once);
        reviewRepositoryMock.Verify(x => x.UpdateAsync(existingReview), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ReviewDoesNotExists_ShouldThrowReviewNotFoundException()
    {
        //Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var reviewDetailsDto = new ReviewDetailsDto
        {
            Id = reviewId,
            Name = "Test",
            Email = "test@test.com",
            Content = "Test",
            Rating = 3,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate(),
            UserId = userId,
            ProductId = productId
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        reviewRepositoryMock
            .Setup(x => x.GetAsync(reviewId))
            .ReturnsAsync((Review) null);

        var reviewService = new ReviewService(reviewRepositoryMock.Object, _clock, productRepositoryMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => reviewService.UpdateAsync(reviewDetailsDto));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<ReviewNotFoundException>();
    }

    [Fact]
    public async Task UpdateAsync_GivenDifferentUserId_ShouldThrowReviewUpdateAuthorizationException()
    {
        //Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var reviewDetailsDto = new ReviewDetailsDto
        {
            Id = reviewId,
            Name = "Test",
            Email = "test@test.com",
            Content = "Test",
            Rating = 3,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate(),
            UserId = userId,
            ProductId = productId
        };

        var existingReview = new Review
        {
            Id = reviewId,
            Name = "Test",
            Email = "test@test.com",
            Content = "Test",
            Rating = 3,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate(),
            UserId = Guid.NewGuid(),
            ProductId = productId
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        reviewRepositoryMock
            .Setup(x => x.GetAsync(reviewId))
            .ReturnsAsync(existingReview);

        var reviewService = new ReviewService(reviewRepositoryMock.Object, _clock, productRepositoryMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => reviewService.UpdateAsync(reviewDetailsDto));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<ReviewUpdateAuthorizationException>();
    }

    [Fact]
    public async Task DeleteAsync_GivenValidReviewIdAndUserId_ShouldDeleteReviewSuccessfully()
    {
        //Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var existingReview = new Review
        {
            Id = reviewId,
            Name = "Test",
            Email = "test@test.com",
            Content = "Test",
            Rating = 3,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate(),
            UserId = userId,
            ProductId = productId
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        reviewRepositoryMock
            .Setup(x => x.GetAsync(reviewId))
            .ReturnsAsync(existingReview);

        var reviewService = new ReviewService(reviewRepositoryMock.Object, _clock, productRepositoryMock.Object);

        //Act
        await reviewService.DeleteAsync(reviewId, userId);

        //Assert
        reviewRepositoryMock.Verify(x => x.GetAsync(reviewId), Times.Once);
        reviewRepositoryMock.Verify(x => x.DeleteAsync(existingReview), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ReviewDoesNotExists_ShouldThrowReviewNotFoundException()
    {
        //Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var productRepositoryMock = new Mock<IProductRepository>();
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        reviewRepositoryMock
            .Setup(x => x.GetAsync(reviewId))
            .ReturnsAsync((Review) null);

        var reviewService = new ReviewService(reviewRepositoryMock.Object, _clock, productRepositoryMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => reviewService.DeleteAsync(reviewId, userId));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<ReviewNotFoundException>();
    }

    [Fact]
    public async Task DeleteAsync_GivenDifferentUserId_ShouldThrowReviewDeleteAuthorizationException()
    {
        //Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var existingReview = new Review
        {
            Id = reviewId,
            Name = "Test",
            Email = "test@test.com",
            Content = "Test",
            Rating = 3,
            CreatedAt = _clock.CurrentDate(),
            ModifiedAt = _clock.CurrentDate(),
            UserId = Guid.NewGuid(),
            ProductId = productId
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        reviewRepositoryMock
            .Setup(x => x.GetAsync(reviewId))
            .ReturnsAsync(existingReview);

        var reviewService = new ReviewService(reviewRepositoryMock.Object, _clock, productRepositoryMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => reviewService.DeleteAsync(reviewId, userId));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<ReviewDeleteAuthorizationException>();
    }
}