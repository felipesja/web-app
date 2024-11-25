using Microsoft.EntityFrameworkCore;
using WebApp.Web.Data;
using WebApp.Web.Domain;

namespace WebApp.Web.Services;

public class ProductService : IProductService
{
    private readonly WebAppDbContext _dbContext;
    public ProductService(WebAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Product>> GetProductsAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
}