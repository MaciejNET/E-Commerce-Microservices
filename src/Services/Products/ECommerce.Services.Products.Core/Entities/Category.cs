namespace ECommerce.Services.Products.Core.Entities;

internal class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Product> Products { get; set; }
}