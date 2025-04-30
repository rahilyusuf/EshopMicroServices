namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<List<GetProductResponse>>;
}
