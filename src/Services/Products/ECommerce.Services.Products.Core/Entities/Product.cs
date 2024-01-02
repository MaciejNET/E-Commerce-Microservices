using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Products.Core.Entities;

internal class Product
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Description { get; set; }
    public string Sku { get; set; }
    public Price StandardPrice { get; set; }
    public Price? DiscountedPrice { get; set; }
    public int StockQuantity { get; set; }
    public string ImageUrl { get; set; }

    public bool IsAvailable
    {
        get => StockQuantity > 0 && !IsDeleted;
        private set => value = StockQuantity > 0 && !IsDeleted;
    }

    public bool IsDeleted { get; set; } = false;
}