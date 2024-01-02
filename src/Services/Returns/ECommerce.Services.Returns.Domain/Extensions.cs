using ECommerce.Services.Returns.Domain.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Services.Returns.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddSingleton<IReturnPolicyFactory, ReturnPolicyFactory>();
        return services;
    }
}