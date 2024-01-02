using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Domain.Carts.Entities;

public class Discount : AggregateRoot
{
    private readonly List<Product> _products;

    private Discount(AggregateId id, string code, int percentage, IReadOnlyCollection<Product> products)
    {
        Id = id;
        Code = code;
        Percentage = percentage;
        _products = products.ToList();
    }

    private Discount()
    {
    }

    public string Code { get; private set; }
    public int Percentage { get; private set; }
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    public DiscountType Type => Products.Count == 0 ? DiscountType.Cart : DiscountType.Product;

    public static Discount Create(AggregateId id, string code, int percentage, IReadOnlyCollection<Product> products)
    {
        return new Discount(id, code, percentage, products);
    }
}

public enum DiscountType
{
    Cart,
    Product
}