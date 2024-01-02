using System.Runtime.CompilerServices;
using ECommerce.Services.Products.Core.DAL;
using ECommerce.Services.Products.Core.DAL.Repositories;
using ECommerce.Services.Products.Core.Repositories;
using ECommerce.Services.Products.Core.Services;
using ECommerce.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ECommerce.Services.Products.Api")]
[assembly: InternalsVisibleTo("ECommerce.Services.Products.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace ECommerce.Services.Products.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<ProductsDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}