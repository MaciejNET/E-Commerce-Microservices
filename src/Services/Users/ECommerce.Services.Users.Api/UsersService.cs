using ECommerce.Services.Users.Core;
using ECommerce.Shared.Abstractions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Services.Users.Api;

internal class UsersService : IService
{
    public const string BasePath = "users-service";
    public string Name { get; } = "Users";
    public string Path => BasePath;
    public IEnumerable<string> Policies => new[] {"users"};

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}