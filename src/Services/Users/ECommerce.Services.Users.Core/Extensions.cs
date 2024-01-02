using System.Runtime.CompilerServices;
using ECommerce.Services.Users.Core.DAL;
using ECommerce.Services.Users.Core.DAL.Repositories;
using ECommerce.Services.Users.Core.Entities;
using ECommerce.Services.Users.Core.Repositories;
using ECommerce.Services.Users.Core.Services;
using ECommerce.Services.Users.Core.Validators;
using ECommerce.Shared.Infrastructure.Postgres;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ECommerce.Services.Users.Api")]
[assembly: InternalsVisibleTo("ECommerce.Modules.Users.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace ECommerce.Services.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<UsersDbContext>();
        services.AddSingleton<PasswordValidator>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddTransient<IIdentityService, IdentityService>();
        return services;
    }
}