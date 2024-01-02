using Convey.CQRS.Events;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Orders.Application.Carts.Events.External.Handlers;

public class ProductUpdatedHandler : IEventHandler<ProductUpdated>
{
    private readonly ILogger<ProductUpdatedHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductUpdatedHandler(IProductRepository productRepository, ILogger<ProductUpdatedHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task HandleAsync(ProductUpdated @event, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(@event.Id);

        if (product is null)
        {
            _logger.LogError("Product with ID: '{ProductId}' was not found", @event.Id);
            return;
        }

        product.SetPrice(@event.Price);
        product.SetStockQuantity(@event.StockQuantity);

        await _productRepository.UpdateAsync(product);
        _logger.LogInformation("Product with ID: '{ProductId}' has been updated", @event.Id);
    }
}