using Convey.CQRS.Events;
using ECommerce.Services.Discounts.Core.Entities;
using ECommerce.Services.Discounts.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Discounts.Core.Events.External.Handlers;

internal sealed class ProductDeletedHandler : IEventHandler<ProductDeleted>
{
    private readonly ILogger<ProductDeletedHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductDeletedHandler(IProductRepository productRepository, ILogger<ProductDeletedHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task HandleAsync(ProductDeleted @event, CancellationToken cancellationToken)
    {
        var product = new Product {Id = @event.Id};

        await _productRepository.DeleteAsync(product);
        _logger.LogInformation("Deleted a product with ID: \'{ProductId}\'", @event.Id);
    }
}