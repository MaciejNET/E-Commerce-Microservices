using ECommerce.Services.Returns.Domain.Exceptions;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Returns.Domain.Entities;

public class Order : AggregateRoot
{
    private readonly List<OrderProduct> _products;

    private Order(AggregateId id, List<OrderProduct> products, DateTime orderPlaced, OrderStatus status)
    {
        Id = id;
        _products = products;
        OrderPlaced = orderPlaced;
        Status = status;
    }

    private Order()
    {
    }

    public IReadOnlyCollection<OrderProduct> Products => _products.AsReadOnly();
    public DateTime OrderPlaced { get; private set; }
    public DateTime? CompletionDate { get; private set; }
    public OrderStatus Status { get; private set; }

    public static Order Create(AggregateId id, List<OrderProduct> products, DateTime orderPlaced, OrderStatus status)
    {
        return new Order(id, products, orderPlaced, status);
    }

    public void StartProcessing()
    {
        if (Status is not OrderStatus.Placed)
            throw new InvalidStatusChangeException(Status.ToString(), OrderStatus.InProgress.ToString());

        Status = OrderStatus.InProgress;
    }

    public void Send()
    {
        if (Status is not OrderStatus.InProgress)
            throw new InvalidStatusChangeException(Status.ToString(), OrderStatus.Sent.ToString());

        Status = OrderStatus.Sent;
    }

    public void Complete(DateTime now)
    {
        if (Status is not OrderStatus.Sent)
            throw new InvalidStatusChangeException(Status.ToString(), OrderStatus.Completed.ToString());

        Status = OrderStatus.Completed;
        CompletionDate = now;
    }

    public void Cancel()
    {
        if (Status is OrderStatus.Completed or OrderStatus.Sent)
            throw new InvalidStatusChangeException(Status.ToString(), OrderStatus.Canceled.ToString());

        Status = OrderStatus.Canceled;
    }

    public void PartlyReturn()
    {
        if (Status is not (OrderStatus.Completed or OrderStatus.PartlyReturned))
            throw new InvalidStatusChangeException(Status.ToString(), OrderStatus.PartlyReturned.ToString());

        Status = OrderStatus.PartlyReturned;
    }

    public void Return()
    {
        if (Status is not (OrderStatus.Completed or OrderStatus.PartlyReturned))
            throw new InvalidStatusChangeException(Status.ToString(), OrderStatus.PartlyReturned.ToString());

        Status = OrderStatus.Returned;
    }
}