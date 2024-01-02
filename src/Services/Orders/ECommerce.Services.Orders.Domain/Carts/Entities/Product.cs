using ECommerce.Services.Orders.Domain.Carts.Exceptions;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Domain.Carts.Entities;

public class Product : AggregateRoot
{
    public Product(AggregateId id, string name, string sku, Price standardPrice, int stockQuantity,
        Price? discountedPrice = null)
    {
        Id = id;
        Name = name;
        Sku = sku;
        StandardPrice = standardPrice;
        DiscountedPrice = discountedPrice;
        StockQuantity = stockQuantity;
    }

    private Product()
    {
    }

    public string Name { get; private set; }
    public string Sku { get; private set; }
    public Price StandardPrice { get; private set; }
    public Price? DiscountedPrice { get; private set; }
    public int StockQuantity { get; private set; }

    public void AddStock(int quantity)
    {
        StockQuantity += quantity;
        IncrementVersion();
    }

    public void DecreaseStock(int quantity)
    {
        if (StockQuantity - quantity < 0) throw new InvalidProductStockQuantityException();

        StockQuantity -= quantity;
        IncrementVersion();
    }

    public void SetPrice(Price price)
    {
        StandardPrice = price;
        IncrementVersion();
    }

    public void SetDiscountedPrice(Price discountedPrice)
    {
        DiscountedPrice = discountedPrice;
        IncrementVersion();
    }

    public void SetStockQuantity(int stockQuantity)
    {
        StockQuantity = stockQuantity;
        IncrementVersion();
    }
}