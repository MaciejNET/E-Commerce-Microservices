using ECommerce.Services.Returns.Domain.Events;
using ECommerce.Services.Returns.Domain.Exceptions;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Returns.Domain.Entities;

public class Return : AggregateRoot
{
    private Return(AggregateId id, UserId userId, Order order, OrderProduct orderProduct, ReturnType type)
    {
        Id = id;
        UserId = userId;
        Order = order;
        OrderProductId = orderProduct.Id;
        OrderProduct = orderProduct;
        Type = type;
        ReturnStatus = ReturnStatus.Pending;
    }

    private Return()
    {
    }

    public UserId UserId { get; set; }
    public Order Order { get; }
    public EntityId OrderProductId { get; private set; }
    public OrderProduct OrderProduct { get; }
    public ReturnType Type { get; private set; }
    public ReturnStatus ReturnStatus { get; private set; }

    public static Return Create(AggregateId id, UserId userId, Order order, OrderProduct orderProduct, ReturnType type)
    {
        return new Return(id, userId, order, orderProduct, type);
    }

    public void ChangeStatus(ReturnStatus status)
    {
        if (status is ReturnStatus.Pending)
            throw new InvalidStatusChangeException(ReturnStatus.ToString(), status.ToString());

        if (ReturnStatus is ReturnStatus.Accepted or ReturnStatus.Declined or ReturnStatus.Returned)
            throw new InvalidStatusChangeException(ReturnStatus.ToString(), status.ToString());

        ReturnStatus = status;
        AddEvent(new ReturnStatusChanged(Order.Id, OrderProduct.Id, status));
    }
}

public enum ReturnType
{
    Return,
    Guarantee
}

public enum ReturnStatus
{
    Pending,
    Accepted,
    Declined,
    Returned,
    SendToManualCheck
}