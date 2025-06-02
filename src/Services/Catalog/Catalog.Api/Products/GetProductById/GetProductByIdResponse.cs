namespace Catalog.Api.Products.GetProductById
{
    public record GetProductByIdResponse(
        Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageUrl,
        decimal Price
    );
}