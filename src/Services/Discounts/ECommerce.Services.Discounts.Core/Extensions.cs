using System.Runtime.CompilerServices;
using ECommerce.Services.Discounts.Core.BackgroundServices;
using ECommerce.Services.Discounts.Core.DAL;
using ECommerce.Services.Discounts.Core.DAL.Repositories;
using ECommerce.Services.Discounts.Core.Repositories;
using ECommerce.Services.Discounts.Core.Services;
using ECommerce.Services.Discounts.Core.Validators;
using ECommerce.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ECommerce.Services.Discounts.Api")]
[assembly: InternalsVisibleTo("ECommerce.Services.Discounts.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace ECommerce.Services.Discounts.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<DiscountDateValidator>();
        services.AddPostgres<DiscountsDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IDiscountCodeRepository, DiscountCodeRepository>();
        services.AddScoped<IProductDiscountRepository, ProductDiscountRepository>();
        services.AddScoped<IProductDiscountService, ProductDiscountService>();
        services.AddScoped<IDiscountCodeService, DiscountCodeService>();
        services.AddHostedService<ProductDiscountAddedService>();
        services.AddHostedService<DiscountCodeExpirationService>();
        services.AddHostedService<ProductDiscountExpirationService>();

        return services;
    }
}