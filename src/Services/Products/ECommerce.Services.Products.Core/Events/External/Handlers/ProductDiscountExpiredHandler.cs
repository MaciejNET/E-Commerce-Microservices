using Convey.CQRS.Events;
using ECommerce.Services.Products.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Products.Core.Events.External.Handlers;

internal class ProductDiscountExpiredHandler : IEventHandler<ProductDiscountExpired>
{
    private readonly ILogger<ProductDiscountExpiredHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductDiscountExpiredHandler(IProductRepository productRepository,
        ILogger<ProductDiscountExpiredHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task HandleAsync(ProductDiscountExpired @event, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(@event.ProductId);

        if (product is null)
        {
            _logger.LogError("Product with ID: '{ProductId}' does not exists", @event.ProductId);
            return;
        }

        product.DiscountedPrice = null;
        await _productRepository.UpdateAsync(product);
        _logger.LogInformation("Discount expired for product with ID: '{ProductId}'", @event.ProductId);
    }
}