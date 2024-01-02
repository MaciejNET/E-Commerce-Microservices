using Convey.CQRS.Events;
using ECommerce.Services.Discounts.Core.Entities;
using ECommerce.Services.Discounts.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Discounts.Core.Events.External.Handlers;

internal sealed class ProductCreatedHandler : IEventHandler<ProductCreated>
{
    private readonly ILogger<ProductCreatedHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductCreatedHandler(IProductRepository productRepository, ILogger<ProductCreatedHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task HandleAsync(ProductCreated @event, CancellationToken cancellationToken)
    {
        var product = new Product {Id = @event.Id};

        await _productRepository.AddAsync(product);
        _logger.LogInformation("Added a product with ID: \'{ProductId}\'", @event.Id);
    }
}