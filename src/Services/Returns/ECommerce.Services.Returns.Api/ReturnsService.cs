using Convey.CQRS.Events;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using ECommerce.Services.Returns.Application;
using ECommerce.Services.Returns.Application.Events.External;
using ECommerce.Services.Returns.Domain;
using ECommerce.Services.Returns.Infrastructure;
using ECommerce.Shared.Abstractions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Services.Returns.Api;

internal class ReturnsService : IService
{
    public const string BasePath = "returns-service";
    public string Name { get; } = "Returns";
    public string Path => BasePath;
    public IEnumerable<string> Policies => new[] {"returns"};

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
            .SubscribeEvent<OrderCanceled>()
            .SubscribeEvent<OrderCompleted>()
            .SubscribeEvent<OrderPlaced>()
            .SubscribeEvent<OrderSent>()
            .SubscribeEvent<OrderStartedProcessing>();
    }
}