namespace Catalog.Api.Products.GetProducts
{
    public record GetProductResponse
    (
        Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageUrl,
        decimal Price
    );
}
