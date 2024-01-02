using Convey.CQRS.Events;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Orders.Application.Carts.Events.External.Handlers;

public class ProductDiscountExpiredHandler : IEventHandler<ProductDiscountExpired>
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
            _logger.LogError("Product with ID: '{ProductId}' was not found", @event.ProductId.ToString());
            return;
        }

        await _productRepository.DeleteAsync(product);
        _logger.LogInformation("Product with ID: '{ProductId}' has been removed", @event.ProductId);
    }
}