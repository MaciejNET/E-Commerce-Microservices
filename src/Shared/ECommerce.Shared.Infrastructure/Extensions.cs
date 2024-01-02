using System.Runtime.CompilerServices;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.RabbitMQ;
using ECommerce.Shared.Abstractions.Time;
using ECommerce.Shared.Infrastructure.Api;
using ECommerce.Shared.Infrastructure.Auth;
using ECommerce.Shared.Infrastructure.Contexts;
using ECommerce.Shared.Infrastructure.Exceptions;
using ECommerce.Shared.Infrastructure.Kernel;
using ECommerce.Shared.Infrastructure.Postgres;
using ECommerce.Shared.Infrastructure.Services;
using ECommerce.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ECommerce.Shared.Infrastructure;

public static class Extensions
{
    private const string CorsPolicy = "cors";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IEnumerable<string> policies)
    {
        using var serviceProvider = services.BuildServiceProvider();
        services.AddCors(cors =>
        {
            cors.AddPolicy(CorsPolicy, x =>
            {
                x.WithOrigins("*")
                    .WithMethods("POST", "PUT", "DELETE")
                    .WithHeaders("Content-Type", "Authorization");
            });
        });

        services.AddSwaggerGen(swagger =>
        {
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ECommerce API",
                Version = "v1"
            });
        });

        services.AddConvey()
            .AddRabbitMq("rabbitMq")
            .AddCommandHandlers()
            .AddInMemoryCommandDispatcher()
            .AddQueryHandlers()
            .AddInMemoryQueryDispatcher()
            .AddEventHandlers()
            .AddInMemoryEventDispatcher()
            .Build();
        
        services.AddSingleton<IContextFactory, ContextFactory>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());
        services.AddAuth(policies);
        services.AddDomainEvents();
        services.AddErrorHandling();
        services.AddPostgres();
        services.AddSingleton<IClock, UtcClock>();
        services.AddHostedService<AppInitializer>();
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });
        services.AddEndpointsApiExplorer();
        
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseCors(CorsPolicy);
        app.UseErrorHandling();
        app.UseSwagger();
        app.UseSwaggerUI(swagger =>
        {
            swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API V1");
            swagger.DocumentTitle = "ECommerce API";
        });
        app.MapControllers();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseConvey()
            .UseRabbitMq();
        return app;
    }
    
    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}