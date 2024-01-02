using Convey.CQRS.Events;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Orders.Application.Carts.Events.External.Handlers;

public class DiscountCodeExpiredHandler : IEventHandler<DiscountCodeExpired>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountCodeExpiredHandler> _logger;

    public DiscountCodeExpiredHandler(IDiscountRepository discountRepository,
        ILogger<DiscountCodeExpiredHandler> logger)
    {
        _discountRepository = discountRepository;
        _logger = logger;
    }

    public async Task HandleAsync(DiscountCodeExpired @event, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetAsync(@event.Id);

        if (discount is null)
        {
            _logger.LogError("Discount with ID: '{DiscountId}' was not found", @event.Id.ToString());
            return;
        }

        await _discountRepository.DeleteAsync(discount);
        _logger.LogInformation("Discount with Id: {DiscountId} has been removed", @event.Id.ToString());
    }
}