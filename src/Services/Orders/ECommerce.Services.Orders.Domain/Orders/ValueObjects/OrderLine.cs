using ECommerce.Services.Orders.Domain.Orders.Exceptions;
using ECommerce.Shared.Abstractions.Kernel.Enums;

namespace ECommerce.Services.Orders.Domain.Orders.ValueObjects;

public record OrderLine
{
    public OrderLine(int orderLineNumber, string sku, string name, decimal unitPrice, Currency currency, int quantity)
    {
        if (quantity < 1) throw new InvalidOrderLineException(nameof(quantity));

        if (unitPrice < 0) throw new InvalidOrderLineException(nameof(unitPrice));

        OrderLineNumber = orderLineNumber;
        Sku = sku;
        Name = name;
        UnitPrice = unitPrice;
        Currency = currency.ToString();
        Quantity = quantity;
    }

    private OrderLine()
    {
    }

    public int OrderLineNumber { get; private set; }
    public string Sku { get; private set; }
    public string Name { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string Currency { get; set; }
    public int Quantity { get; private set; }
}