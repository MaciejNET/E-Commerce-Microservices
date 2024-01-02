using ECommerce.Services.Orders.Application.Carts.DTO;
using ECommerce.Services.Orders.Domain.Carts.Entities;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Mappings;

public static class CartsExtensions
{
    internal static ProductDto AsDto(this Product product)
    {
        return new ProductDto(
            product.Id,
            product.Name,
            product.Sku,
            product.StandardPrice,
            product.DiscountedPrice
        );
    }

    internal static CartItemDto AsDto(this CartItem cartItem)
    {
        return new CartItemDto(
            cartItem.Id,
            cartItem.Quantity,
            cartItem.Product.AsDto()
        );
    }

    internal static CheckoutCartItemDto AsDto(this CheckoutCartItem checkoutCartItem)
    {
        return new CheckoutCartItemDto(
            checkoutCartItem.Id,
            checkoutCartItem.Quantity,
            checkoutCartItem.Price,
            checkoutCartItem.DiscountedPrice
        );
    }

    public static CartDto AsDto(this Cart cart)
    {
        return new CartDto(
            cart.Id,
            cart.Items.Select(AsDto)
        );
    }

    internal static DiscountDto AsDto(this Discount discount)
    {
        return new DiscountDto(
            discount.Id,
            discount.Code,
            discount.Percentage
        );
    }

    public static CheckoutCartDto AsDto(this CheckoutCart checkoutCart)
    {
        return new CheckoutCartDto(
            checkoutCart.Id,
            checkoutCart.Payment,
            checkoutCart.Shipment.AsDto(),
            checkoutCart.Discount.AsDto(),
            checkoutCart.Items.Select(AsDto)
        );
    }
}