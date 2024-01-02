using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Returns.Domain.Entities;

public class OrderProduct
{
    private OrderProduct(string sku, AggregateId orderId)
    {
        Id = new EntityId(Guid.NewGuid());
        Sku = sku;
        OrderId = orderId;
        IsReturn = false;
    }

    private OrderProduct()
    {
    }

    public EntityId Id { get; private set; }
    public string Sku { get; private set; }
    public AggregateId OrderId { get; private set; }
    public bool IsReturn { get; private set; }

    public static OrderProduct Create(string sku, AggregateId orderId)
    {
        return new OrderProduct(sku, orderId);
    }

    public void Return()
    {
        IsReturn = true;
    }
}