using Convey;
using Convey.CQRS.Queries;
using ECommerce.Modules.Orders.Infrastructure.EF;
using ECommerce.Modules.Orders.Infrastructure.EF.Repositories;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Services.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Modules.Orders.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<OrdersDbContext>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICheckoutCartRepository, CheckoutCartRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICheckoutCartItemRepository, CheckoutCartItemRepository>();
        services.AddConvey().AddQueryHandlers().AddInMemoryQueryDispatcher();
        return services;
    }
}