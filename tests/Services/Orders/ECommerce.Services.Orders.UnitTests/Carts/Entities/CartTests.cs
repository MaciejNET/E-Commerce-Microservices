using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Services.Orders.Domain.Carts.Events;
using ECommerce.Services.Orders.Domain.Carts.Exceptions;
using ECommerce.Services.Orders.Domain.Carts.Services;
using ECommerce.Services.Orders.Domain.Orders.Exceptions;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Types;
using FluentAssertions;

namespace ECommerce.Modules.Orders.UnitTests.Carts.Entities;

public class CartTests
{
    [Fact]
    public void AddItem_ExistingItem_ShouldIncreasesQuantitySuccessfully()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()), Currency.PLN);
        var product = new Product(new AggregateId(), "Product 1", "SKU123", new Price(10, Currency.PLN), 20);
        cart.AddItem(product, 2);

        cart.AddItem(product, 3);

        var addedItem = cart.Items.Single();
        addedItem.Quantity.Should().Be(5);
    }

    [Fact]
    public void AddItem_NewItem_ShouldAddsItemToCartSuccessfully()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()), Currency.PLN);
        var product = new Product(new AggregateId(), "Product 1", "SKU123", new Price(10, Currency.PLN), 20);

        cart.AddItem(product, 2);

        cart.Items.Should().HaveCount(1);
        cart.Items.First().Product.Should().Be(product);
        cart.Items.First().Quantity.Should().Be(2);
    }

    [Fact]
    public void RemoveItem_ExistingItem_ShouldRemovesItemFromCartSuccessfully()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()), Currency.PLN);
        var product = new Product(new AggregateId(), "Product 1", "SKU123", new Price(10, Currency.PLN), 20);
        cart.AddItem(product, 2);

        cart.RemoveItem(product);

        cart.Items.Should().BeEmpty();
    }

    [Fact]
    public void RemoveItem_NonexistentItem_ShouldThrowsCartItemNotFoundException()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()), Currency.PLN);
        var product = new Product(new AggregateId(), "Product 1", "SKU123", new Price(10, Currency.PLN), 20);

        cart.Invoking(c => c.RemoveItem(product))
            .Should().Throw<CartItemNotFoundException>();
    }

    [Fact]
    public void Clear_EmptyCart_ShouldAddsCartClearedEventSuccessfully()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()), Currency.PLN);

        cart.Clear();

        cart.Events.Should().ContainSingle(e => e is CartCleared);
    }

    [Fact]
    public void Checkout_EmptyCart_ShouldThrowsCannotCheckoutEmptyCartException()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()), Currency.PLN);
        var exchangeRateService = new FakeExchangeRateService();
        cart.Invoking(c => c.Checkout(exchangeRateService))
            .Should().ThrowAsync<CannotCheckoutEmptyCartException>();
    }

    [Fact]
    public void Checkout_NotEnoughStock_ShouldThrowsNotEnoughProductsInStockException()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()), Currency.PLN);
        var product = new Product(new AggregateId(), "Product 1", "SKU123", new Price(10, Currency.PLN), 3);
        var exchangeRateService = new FakeExchangeRateService();
        cart.AddItem(product, 5);

        cart.Invoking(c => c.Checkout(exchangeRateService))
            .Should().ThrowAsync<NotEnoughProductsInStockException>();
    }

    [Fact]
    public async Task Checkout_ValidCart_ShouldAddsCartCheckedOutEventSuccessfully()
    {
        var cart = Cart.Create(new AggregateId(), new UserId(Guid.NewGuid()), Currency.PLN);
        var product = new Product(new AggregateId(), "Product 1", "SKU123", new Price(10, Currency.PLN), 5);
        var exchangeRateService = new FakeExchangeRateService();
        cart.AddItem(product, 3);

        await cart.Checkout(exchangeRateService);
        cart.Events.Should().ContainSingle(e => e is CartCheckedOut);
    }
}