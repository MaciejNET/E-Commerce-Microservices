using Convey.CQRS.Queries;
using ECommerce.Services.Orders.Application.Orders.DTO;

namespace ECommerce.Services.Orders.Application.Orders.Queries;

public sealed class GetOrder : IQuery<OrderDto>
{
    public Guid Id { get; set; }
}