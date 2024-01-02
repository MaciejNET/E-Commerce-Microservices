using Convey.CQRS.Queries;
using ECommerce.Services.Returns.Application.DTO;

namespace ECommerce.Services.Returns.Application.Queries;

public class GetReturnsToCheck : IQuery<IEnumerable<ReturnDto>>
{
}