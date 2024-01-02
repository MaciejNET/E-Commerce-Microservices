using System.Runtime.CompilerServices;
using ECommerce.Services.Orders.Domain.Carts.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ECommerce.Services.Orders.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyAssemblyGen2")]

namespace ECommerce.Services.Orders.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IExchangeRateService, FakeExchangeRateService>();
        return services;
    }
}