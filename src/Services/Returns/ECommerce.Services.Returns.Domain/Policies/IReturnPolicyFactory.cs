using ECommerce.Services.Returns.Domain.Entities;

namespace ECommerce.Services.Returns.Domain.Policies;

public interface IReturnPolicyFactory
{
    IReturnPolicy Get(ReturnType type);
}