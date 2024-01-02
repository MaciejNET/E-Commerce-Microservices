namespace ECommerce.Shared.Abstractions.Kernel.Types;

public class UserId : TypeId
{
    public UserId(Guid value) : base(value)
    {
    }

    public static implicit operator UserId(Guid id)
        => new(id);
    
    private UserId() : base() {}
}