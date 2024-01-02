using System.ComponentModel.DataAnnotations;

namespace ECommerce.Services.Discounts.Core.DTO;

internal class DiscountCodeDto
{
    public Guid Id { get; set; }

    [StringLength(50, MinimumLength = 3)] public string Code { get; set; }

    public string Description { get; set; }

    [Range(1, 99)] public int Percentage { get; set; }

    public List<Guid> ProductIds { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}