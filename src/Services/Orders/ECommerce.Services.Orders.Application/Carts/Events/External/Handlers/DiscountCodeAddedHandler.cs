using Convey.CQRS.Events;
using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Orders.Application.Carts.Events.External.Handlers;

public class DiscountCodeAddedHandler : IEventHandler<DiscountCodeAdded>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountCodeAddedHandler> _logger;
    private readonly IProductRepository _productRepository;

    public DiscountCodeAddedHandler(IProductRepository productRepository, IDiscountRepository discountRepository,
        ILogger<DiscountCodeAddedHandler> logger)
    {
        _productRepository = productRepository;
        _discountRepository = discountRepository;
        _logger = logger;
    }

    public async Task HandleAsync(DiscountCodeAdded @event, CancellationToken cancellationToken)
    {
        var products = await _productRepository.BrowsAsync(@event.ProductIds);

        var discount = Discount.Create(
            @event.Id,
            @event.Code,
            @event.Percentage,
            products.ToList());

        await _discountRepository.AddAsync(discount);
        _logger.LogInformation("Added discount code with ID: '{DiscountId}' and code: '{Code}'", discount.Id.ToString(),
            discount.Code);
    }
}