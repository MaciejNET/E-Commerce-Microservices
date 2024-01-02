using Convey.CQRS.Events;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using ECommerce.Services.Products.Core;
using ECommerce.Services.Products.Core.Events.External;
using ECommerce.Shared.Abstractions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Services.Products.Api;

internal class ProductsService : IService
{
    public const string BasePath = "products-service";
    public string Name { get; } = "Products";
    public string Path => BasePath;
    public IEnumerable<string> Policies => new[] {"products", "categories"};

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseRabbitMq()
            .SubscribeEvent<ProductBought>()
            .SubscribeEvent<ProductDiscountAdded>()
            .SubscribeEvent<ProductDiscountExpired>();
    }
}