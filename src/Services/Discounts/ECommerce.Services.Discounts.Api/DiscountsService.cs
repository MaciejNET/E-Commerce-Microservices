using Convey.CQRS.Events;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using ECommerce.Services.Discounts.Core;
using ECommerce.Services.Discounts.Core.Events.External;
using ECommerce.Shared.Abstractions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Services.Discounts.Api;

internal class DiscountsService : IService
{
    public const string BasePath = "discounts-service";
    public string Name { get; } = "Discounts";
    public string Path => BasePath;
    public IEnumerable<string> Policies => new[] {"discounts"};

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseRabbitMq()
            .SubscribeEvent<ProductCreated>()
            .SubscribeEvent<ProductDeleted>();
    }
}