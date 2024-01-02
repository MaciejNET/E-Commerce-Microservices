using Convey.CQRS.Queries;
using ECommerce.Services.Returns.Application.DTO;

namespace ECommerce.Services.Returns.Application.Queries;

public class GetUserReturns : IQuery<IEnumerable<ReturnDto>>
{
    public Guid Id { get; set; }
}