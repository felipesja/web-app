using webapp.Model;

namespace webapp.Services {
    public interface IProductService {

        IList<Product> GetProducts();
        public Product GetProductById(int id);
    }
}
