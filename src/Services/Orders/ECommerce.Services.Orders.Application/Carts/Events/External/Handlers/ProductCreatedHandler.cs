using Convey.CQRS.Events;
using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Orders.Application.Carts.Events.External.Handlers;

public class ProductCreatedHandler : IEventHandler<ProductCreated>
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
        var product = await _productRepository.GetAsync(@event.Id);

        if (product is not null)
        {
            _logger.LogError("Product with ID: '{ProductId}' already exists", product.Id.ToString());
            return;
        }

        product = new Product(@event.Id, @event.Name, @event.Sku, @event.Price, @event.StockQuantity);

        await _productRepository.AddAsync(product);
        _logger.LogInformation("Product with ID: {ProductId} has been added", @event.Id);
    }
}