namespace Catalog.Api.Products.CreateProduct
{
        public record class CreateProductRequest(
            string Name, 
            List<string> Category, 
            string Description, 
            string ImageUrl, 
            decimal Price
            );
}
