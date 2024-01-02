using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Products.Core.DTO;

internal class ProductDetailsDto
{
    public Guid Id { get; set; }
    public string Category { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Manufacturer { get; set; }

    [Required]
    [StringLength(250, MinimumLength = 3)]
    public string Description { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 3)]
    public string Sku { get; set; }

    [Required] public Price Price { get; set; }

    public string ImageUrl { get; set; }

    [Range(1, int.MaxValue)] public int StockQuantity { get; set; }
}