using Convey.CQRS.Events;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using ECommerce.Services.Reviews.Core;
using ECommerce.Services.Reviews.Core.Events.External;
using ECommerce.Shared.Abstractions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Services.Reviews.Api;

internal class ReviewsService : IService
{
    public const string BasePath = "reviews-service";
    public string Name { get; } = "Reviews";
    public string Path => BasePath;
    public IEnumerable<string> Policies => new[] {"reviews"};

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