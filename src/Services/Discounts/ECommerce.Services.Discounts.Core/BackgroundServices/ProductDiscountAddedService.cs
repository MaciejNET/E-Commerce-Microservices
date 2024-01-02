using Convey.MessageBrokers;
using ECommerce.Services.Discounts.Core.Events;
using ECommerce.Services.Discounts.Core.Repositories;
using ECommerce.Shared.Abstractions.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Discounts.Core.BackgroundServices;

internal class ProductDiscountAddedService : BackgroundService
{
    private readonly IBusPublisher _busPublisher;
    private readonly IClock _clock;
    private readonly ILogger<ProductDiscountAddedService> _logger;
    private readonly TimeSpan _period = TimeSpan.FromSeconds(60);
    private readonly IServiceProvider _serviceProvider;

    public ProductDiscountAddedService(IBusPublisher busPublisher, ILogger<ProductDiscountAddedService> logger,
        IClock clock, IServiceProvider serviceProvider)
    {
        _busPublisher = busPublisher;
        _logger = logger;
        _clock = clock;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_period);
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = _serviceProvider.CreateScope();
            var productDiscountRepository = scope.ServiceProvider.GetRequiredService<IProductDiscountRepository>();
            var currentDate = _clock.CurrentDate();
            var newDiscounts = await productDiscountRepository.GetNewlyAddedDiscountsAsync(currentDate);

            foreach (var newDiscount in newDiscounts)
            {
                await _busPublisher.PublishAsync(new ProductDiscountAdded(newDiscount.ProductId, newDiscount.NewPrice));
                _logger.LogInformation("New discount added for product with ID: '{ProductId}'", newDiscount.ProductId);
            }
        }
    }
}