namespace ECommerce.Shared.Abstractions.Kernel.Types;

public class OrderId : TypeId
{
    public OrderId(Guid value) : base(value)
    {
    }

    public static implicit operator OrderId(Guid id)
        => new(id);
    
    private OrderId() : base() {}
}