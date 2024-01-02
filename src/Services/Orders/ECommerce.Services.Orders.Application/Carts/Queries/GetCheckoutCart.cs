using Convey.CQRS.Queries;
using ECommerce.Services.Orders.Application.Carts.DTO;

namespace ECommerce.Services.Orders.Application.Carts.Queries;

public class GetCheckoutCart : IQuery<CheckoutCartDto>
{
    public Guid UserId { get; set; }
}