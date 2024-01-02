using Convey.MessageBrokers;
using ECommerce.Services.Discounts.Core.Events;
using ECommerce.Services.Discounts.Core.Repositories;
using ECommerce.Shared.Abstractions.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Discounts.Core.BackgroundServices;

internal class DiscountCodeAddedService : BackgroundService
{
    private readonly IBusPublisher _busPublisher;
    private readonly IClock _clock;
    private readonly ILogger<DiscountCodeAddedService> _logger;
    private readonly TimeSpan _period = TimeSpan.FromSeconds(60);
    private readonly IServiceProvider _serviceProvider;

    public DiscountCodeAddedService(IBusPublisher busPublisher, ILogger<DiscountCodeAddedService> logger, IClock clock,
        IServiceProvider serviceProvider)
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
            var productDiscountRepository = scope.ServiceProvider.GetRequiredService<IDiscountCodeRepository>();
            var currentDate = _clock.CurrentDate();
            var newDiscountCodes = await productDiscountRepository.GetNewlyAddedDiscountCodesAsync(currentDate);

            foreach (var newDiscountCode in newDiscountCodes)
            {
                await _busPublisher.PublishAsync(new DiscountCodeAdded(
                    newDiscountCode.Id,
                    newDiscountCode.Code,
                    newDiscountCode.Percentage, newDiscountCode.Products.Select(x => x.Id).ToList()));
                _logger.LogInformation("New discount code: '{Code}' added", newDiscountCode.Code);
            }
        }
    }
}