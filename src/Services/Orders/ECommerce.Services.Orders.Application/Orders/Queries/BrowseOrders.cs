using Convey.CQRS.Queries;
using ECommerce.Services.Orders.Application.Orders.DTO;

namespace ECommerce.Services.Orders.Application.Orders.Queries;

public sealed class BrowseOrders : IQuery<IEnumerable<OrderDto>>
{
    public Guid? UserId { get; set; }
    public bool? IsCompleted { get; set; }
}