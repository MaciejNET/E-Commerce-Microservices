namespace ECommerce.Services.Discounts.Core.Entities;

internal class DiscountCode
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public int Percentage { get; set; }
    public ICollection<Product> Products { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}