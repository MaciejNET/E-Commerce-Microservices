using ECommerce.Services.Orders.Domain.Carts.Events;
using ECommerce.Services.Orders.Domain.Carts.Exceptions;
using ECommerce.Services.Orders.Domain.Carts.Services;
using ECommerce.Services.Orders.Domain.Orders.Exceptions;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Domain.Carts.Entities;

public class Cart : AggregateRoot
{
    private readonly List<CartItem> _items = new();

    internal Cart(AggregateId id, UserId userId, Currency preferredCurrency)
    {
        (Id, UserId, PreferredCurrency) = (id, userId, preferredCurrency);
    }

    private Cart()
    {
    }

    public UserId UserId { get; private set; }
    public Currency PreferredCurrency { get; private set; }
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public static Cart Create(AggregateId id, UserId userId, Currency preferredCurrency)
    {
        return new Cart(id, userId, preferredCurrency);
    }

    public void AddItem(Product product, int quantity)
    {
        var existingItem = _items.SingleOrDefault(x => x.Product == product);
        if (existingItem is not null)
            existingItem.IncreaseQuantity(quantity);
        else
            _items.Add(CartItem.Create(quantity, product));
        IncrementVersion();
    }

    public void RemoveItem(Product product)
    {
        var item = _items.SingleOrDefault(x => x.Product == product);
        if (item is null) throw new CartItemNotFoundException(product.Id);

        _items.Remove(item);
        IncrementVersion();
    }

    public void Clear()
    {
        _items.Clear();
        AddEvent(new CartCleared(Id, Items));
        IncrementVersion();
    }

    public async Task Checkout(IExchangeRateService exchangeRateService)
    {
        if (Items.Count == 0) throw new CannotCheckoutEmptyCartException();

        List<CheckoutCartItem> checkoutCartItems = new();
        foreach (var item in Items)
        {
            if (item.Product.StockQuantity - item.Quantity < 0)
                throw new NotEnoughProductsInStockException(item.Product.Id);

            var exchangedPrice = item.Product.StandardPrice;
            if (item.Product.StandardPrice.Currency != PreferredCurrency)
                exchangedPrice = await exchangeRateService.Exchange(item.Product.StandardPrice, PreferredCurrency);

            if (item.Product.DiscountedPrice?.Currency != PreferredCurrency && item.Product.DiscountedPrice is not null)
                exchangedPrice = await exchangeRateService.Exchange(item.Product.DiscountedPrice, PreferredCurrency);

            checkoutCartItems.Add(new CheckoutCartItem(item.Quantity, item.Product, exchangedPrice));
        }

        AddEvent(new CartCheckedOut(this, checkoutCartItems));
        IncrementVersion();
    }

    public void ChangePreferredCurrency(Currency preferredCurrency)
    {
        PreferredCurrency = preferredCurrency;
        IncrementVersion();
    }
}