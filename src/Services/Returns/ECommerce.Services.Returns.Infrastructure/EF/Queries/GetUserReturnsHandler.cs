using Convey.CQRS.Queries;
using ECommerce.Services.Returns.Application.DTO;
using ECommerce.Services.Returns.Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Returns.Infrastructure.EF.Queries;

internal sealed class GetUserReturnsHandler : IQueryHandler<GetUserReturns, IEnumerable<ReturnDto>>
{
    private readonly ReturnsDbContext _context;

    public GetUserReturnsHandler(ReturnsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReturnDto>> HandleAsync(GetUserReturns query, CancellationToken cancellationToken)
    {
        return await _context.Returns
            .Include(x => x.Order)
            .Include(x => x.OrderProduct)
            .Where(x => x.UserId == query.Id)
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