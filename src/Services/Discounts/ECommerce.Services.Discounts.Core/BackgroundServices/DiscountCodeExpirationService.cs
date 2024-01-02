using Convey.MessageBrokers;
using ECommerce.Services.Discounts.Core.Events;
using ECommerce.Services.Discounts.Core.Repositories;
using ECommerce.Shared.Abstractions.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Discounts.Core.BackgroundServices;

internal class DiscountCodeExpirationService : BackgroundService
{
    private readonly IBusPublisher _busPublisher;
    private readonly IClock _clock;
    private readonly ILogger<DiscountCodeExpirationService> _logger;
    private readonly TimeSpan _period = TimeSpan.FromSeconds(60);
    private readonly IServiceProvider _serviceProvider;

    public DiscountCodeExpirationService(IBusPublisher busPublisher, ILogger<DiscountCodeExpirationService> logger,
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
            var discountCodeRepository = scope.ServiceProvider.GetRequiredService<IDiscountCodeRepository>();
            var currentDate = _clock.CurrentDate();
            var expiredCodes = await discountCodeRepository.GetExpiredCodesAsync(currentDate);

            foreach (var expiredCode in expiredCodes)
            {
                await _busPublisher.PublishAsync(new DiscountCodeExpired(expiredCode.Id));
                _logger.LogInformation("Expired discount code: '{Code}' with ID: '{Id}' has been processed",
                    expiredCode.Code, expiredCode.Id);
            }
        }
    }
}