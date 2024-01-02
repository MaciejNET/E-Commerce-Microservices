using Convey.CQRS.Queries;
using ECommerce.Services.Orders.Application.Carts.DTO;

namespace ECommerce.Services.Orders.Application.Carts.Queries;

public class GetCart : IQuery<CartDto>
{
    public Guid UserId { get; set; }
}