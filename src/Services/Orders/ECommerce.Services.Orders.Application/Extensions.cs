using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ECommerce.Services.Orders.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyAssemblyGen2")]

namespace ECommerce.Services.Orders.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}