using ECommerce.Services.Products.Core.Entities;
using ECommerce.Services.Products.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Products.Core.DAL.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ProductsDbContext _context;

    public ProductRepository(ProductsDbContext context)
    {
        _context = context;
    }

    public Task<Product> GetAsync(Guid id)
    {
        return _context.Products
            .Include(x => x.Category)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<bool> ExistsAsync(string sku)
    {
        return _context.Products.AnyAsync(x => x.Sku == sku);
    }

    public async Task<IEnumerable<Product>> BrowseAsync(string searchText, Guid? categoryId, decimal? minPrice,
        decimal? maxPrice)
    {
        var query = _context.Products
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.IsAvailable)
            .Include(x => x.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchText))
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{searchText}%"));

        if (categoryId.HasValue) query = query.Where(x => x.CategoryId == categoryId);

        if (minPrice.HasValue) query = query.Where(x => x.StandardPrice.Amount >= minPrice);

        if (maxPrice.HasValue) query = query.Where(x => x.StandardPrice.Amount <= maxPrice);

        return await query.ToListAsync();
    }


    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}