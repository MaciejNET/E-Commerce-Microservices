using Convey.MessageBrokers;
using ECommerce.Services.Discounts.Core.Events;
using ECommerce.Services.Discounts.Core.Repositories;
using ECommerce.Shared.Abstractions.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Discounts.Core.BackgroundServices;

internal class ProductDiscountExpirationService : BackgroundService
{
    private readonly IBusPublisher _busPublisher;
    private readonly IClock _clock;
    private readonly ILogger<ProductDiscountExpirationService> _logger;
    private readonly TimeSpan _period = TimeSpan.FromSeconds(60);
    private readonly IServiceProvider _serviceProvider;

    public ProductDiscountExpirationService(IBusPublisher busPublisher,
        ILogger<ProductDiscountExpirationService> logger, IClock clock, IServiceProvider serviceProvider)
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
            var expiredProducts = await productDiscountRepository.GetExpiredProductsAsync(currentDate);

            foreach (var expiredProduct in expiredProducts)
            {
                await _busPublisher.PublishAsync(new ProductDiscountExpired(expiredProduct.ProductId));
                _logger.LogInformation("Expired discount for product with ID: '{Id}' has been processed",
                    expiredProduct.ProductId);
            }
        }
    }
}