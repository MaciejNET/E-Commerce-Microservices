using ECommerce.Shared.Abstractions.Contexts;

namespace ECommerce.Shared.Infrastructure.Contexts;

internal interface IContextFactory
{
    IContext Create();
}