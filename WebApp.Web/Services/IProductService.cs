using WebApp.Web.Domain;

namespace WebApp.Web.Services
{
    public interface IProductService
    {
        public Task<Product?> GetProductByIdAsync(int id);
        public Task<IList<Product>> GetProductsAsync();
    }
}
