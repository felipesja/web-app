using WebApp.Web.Services;

namespace WebApp.Web.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            app.MapGet("/products", async (IProductService _productService) =>
            {
                return await _productService.GetProductsAsync();
            })
            .WithName("GetProducts")
            .WithOpenApi()
            .RequireAuthorization();

            app.MapGet("/products/{id}", async (IProductService _productService, int id) =>
            {
                var product = await _productService.GetProductByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            })
            .WithName("GetProductById")
            .WithOpenApi();
        }
    }
}
