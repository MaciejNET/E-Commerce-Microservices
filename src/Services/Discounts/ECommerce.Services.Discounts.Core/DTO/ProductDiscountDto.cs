using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Discounts.Core.DTO;

public class ProductDiscountDto
{
    public Guid Id { get; set; }
    public Price NewPrice { get; set; }
    public Guid ProductId { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}