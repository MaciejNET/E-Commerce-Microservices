using ECommerce.Services.Orders.Domain.Carts.Exceptions;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Domain.Carts.Entities;

public class CartItem
{
    private CartItem(int quantity, Product product)
    {
        Id = new EntityId(Guid.NewGuid());
        if (quantity < 1) throw new ArgumentOutOfRangeException(nameof(quantity));
        Quantity = quantity;
        Product = product ?? throw new ArgumentNullException(nameof(product));
    }

    private CartItem()
    {
    }

    public EntityId Id { get; private set; }
    public int Quantity { get; private set; }
    public Product Product { get; private set; }

    internal static CartItem Create(int quantity, Product product)
    {
        var cartItem = new CartItem(quantity, product);

        return cartItem;
    }

    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public void DecreaseQuantity(int quantity)
    {
        if (Quantity - quantity < 0) throw new InvalidItemQuantityException();

        Quantity -= quantity;
    }
}