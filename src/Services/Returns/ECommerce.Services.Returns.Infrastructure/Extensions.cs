using Convey;
using Convey.CQRS.Queries;
using ECommerce.Services.Returns.Domain.Repositories;
using ECommerce.Services.Returns.Infrastructure.EF;
using ECommerce.Services.Returns.Infrastructure.EF.Repositories;
using ECommerce.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Services.Returns.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<ReturnsDbContext>();
        services.AddScoped<IReturnRepository, ReturnRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderProductRepository, OrderProductRepository>();
        services.AddConvey().AddQueryHandlers().AddInMemoryQueryDispatcher();
        return services;
    }
}