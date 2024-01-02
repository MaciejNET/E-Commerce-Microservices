using Convey.CQRS.Events;
using ECommerce.Services.Products.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Products.Core.Events.External.Handlers;

internal sealed class ProductBoughtHandler : IEventHandler<ProductBought>
{
    private readonly ILogger<ProductBoughtHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductBoughtHandler(IProductRepository productRepository, ILogger<ProductBoughtHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task HandleAsync(ProductBought @event, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(@event.ProductId);

        if (product is null)
        {
            _logger.LogError("Product with ID: {ProductId} does not exists", @event.ProductId);
            return;
        }

        product.StockQuantity -= @event.Quantity;
        await _productRepository.UpdateAsync(product);
        _logger.LogInformation("Product with ID '{Product}' has been bought. Stock quantity has been updated",
            @event.ProductId);
    }
}