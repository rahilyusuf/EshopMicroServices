namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<string> Category { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
}
