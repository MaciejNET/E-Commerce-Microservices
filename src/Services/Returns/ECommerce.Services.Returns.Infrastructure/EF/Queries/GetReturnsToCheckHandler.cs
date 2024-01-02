using Convey.CQRS.Queries;
using ECommerce.Services.Returns.Application.DTO;
using ECommerce.Services.Returns.Application.Queries;
using ECommerce.Services.Returns.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Returns.Infrastructure.EF.Queries;

internal sealed class GetReturnsToCheckHandler : IQueryHandler<GetReturnsToCheck, IEnumerable<ReturnDto>>
{
    private readonly ReturnsDbContext _context;

    public GetReturnsToCheckHandler(ReturnsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReturnDto>> HandleAsync(GetReturnsToCheck query, CancellationToken cancellationToken)
    {
        return await _context.Returns
            .Include(x => x.Order)
            .Include(x => x.OrderProduct)
            .Where(x => x.ReturnStatus == ReturnStatus.SendToManualCheck)
            .Select(x =>
                new ReturnDto
                (
                    x.Id,
                    x.UserId,
                    x.Order.Id,
                    x.OrderProduct.Sku,
                    x.Type,
                    x.ReturnStatus
                ))
            .ToListAsync(cancellationToken: cancellationToken);
    }
}