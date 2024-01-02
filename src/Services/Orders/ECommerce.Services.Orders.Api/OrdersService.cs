using Convey.CQRS.Events;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using ECommerce.Modules.Orders.Infrastructure;
using ECommerce.Services.Orders.Application;
using ECommerce.Services.Orders.Application.Carts.Events.External;
using ECommerce.Services.Orders.Domain;
using ECommerce.Shared.Abstractions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Services.Orders.Api;

internal class OrdersService : IService
{
    public const string BasePath = "orders-service";
    public string Name { get; } = "Orders";
    public string Path => BasePath;

    public IEnumerable<string> Policies { get; } = new[]
    {
        "carts", "orders"
    };

    public void Register(IServiceCollection services)
    {
        services
            .AddDomain()
            .AddApplication()
            .AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseRabbitMq()
            .SubscribeEvent<DiscountCodeAdded>()
            .SubscribeEvent<DiscountCodeExpired>()
            .SubscribeEvent<ProductCreated>()
            .SubscribeEvent<ProductDeleted>()
            .SubscribeEvent<ProductDiscountAdded>()
            .SubscribeEvent<ProductDiscountExpired>()
            .SubscribeEvent<ProductUpdated>()
            .SubscribeEvent<SignedUp>();
    }
}