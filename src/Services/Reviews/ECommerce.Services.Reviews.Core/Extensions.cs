using System.Runtime.CompilerServices;
using ECommerce.Services.Reviews.Core.DAL;
using ECommerce.Services.Reviews.Core.DAL.Repositories;
using ECommerce.Services.Reviews.Core.Repositories;
using ECommerce.Services.Reviews.Core.Services;
using ECommerce.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ECommerce.Services.Reviews.Api")]
[assembly: InternalsVisibleTo("ECommerce.Services.Reviews.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace ECommerce.Services.Reviews.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<ReviewsDbContext>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewService, ReviewService>();

        return services;
    }
}