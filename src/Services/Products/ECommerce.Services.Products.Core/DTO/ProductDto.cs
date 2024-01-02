using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Products.Core.DTO;

internal class ProductDto
{
    public Guid Id { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Description { get; set; }
    public string Sku { get; set; }
    public Price StandardPrice { get; set; }
    public Price? DiscountedPrice { get; set; }
    public string ImageUrl { get; set; }
    public bool IsAvailable { get; set; }
}