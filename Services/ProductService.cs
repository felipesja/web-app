using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Model;

namespace webapp.Services
{
    public class ProductService : IProductService
    {
        private readonly WebAppDbContext _dbContext;
        public ProductService(WebAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return new Product
            {
                Id = id,
                Name = "Produto",
                Description = "Descrição Produto",
                Sku = "PRD-01",
                Price = 1500.00m
            };
        }
    }
}
