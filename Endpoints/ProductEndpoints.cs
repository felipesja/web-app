using webapp.Services;

namespace webapp.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            app.MapGet("/products", (IProductService productService) =>
            {
                return productService.GetProducts();
            })
            .WithName("GetProducts")
            .WithOpenApi()
            .RequireAuthorization();

            app.MapGet("/products/{id}", (IProductService productService, int id) =>
            {
                return productService.GetProductById(id);
            })
            .WithName("GetProductById")
            .WithOpenApi();
        }
    }
}
