using System.Reflection;
using ECommerce.Shared.Abstractions.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Infrastructure.Kernel;

internal static class Extensions
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        services.Scan(s => s.FromAssemblies(Assembly.GetCallingAssembly())
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}