namespace Catalog.Api.Products.GetProductByCategory
{
    public record GetProductByCategoryResponse(
         Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageUrl,
        decimal Price
        );
}
