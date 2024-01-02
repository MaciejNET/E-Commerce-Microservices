using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Abstractions.Services;

public interface IService
{
    string Name { get; }
    string Path { get; }
    IEnumerable<string> Policies => null;
    void Register(IServiceCollection services);
    void Use(IApplicationBuilder app);
}