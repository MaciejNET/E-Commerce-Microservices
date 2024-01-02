using ECommerce.Services.Returns.Domain.Entities;

namespace ECommerce.Services.Returns.Domain.Policies;

public class ReturnPolicyFactory : IReturnPolicyFactory
{
    public IReturnPolicy Get(ReturnType type)
    {
        return type switch
        {
            ReturnType.Return => new ReturnTypePolicy(),
            ReturnType.Guarantee => new GuaranteeTypePolicy(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}