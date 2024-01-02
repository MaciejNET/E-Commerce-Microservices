using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Orders.Domain.Carts.Events.Handlers;

public sealed class ProductBoughtHandler : IDomainEventHandler<ProductBought>
{
    private readonly ILogger<ProductBoughtHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductBoughtHandler(IProductRepository productRepository, ILogger<ProductBoughtHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task HandleAsync(ProductBought @event)
    {
        var product = await _productRepository.GetAsync(@event.Id);

        if (product is null)
        {
            _logger.LogError("Product with ID: '{Id}' does not exists", @event.Id);
            return;
        }

        product.DecreaseStock(@event.Quantity);
        await _productRepository.UpdateAsync(product);
        _logger.LogInformation("Product with ID: '{Id}' has been bought", product.Id.ToString());
    }
}