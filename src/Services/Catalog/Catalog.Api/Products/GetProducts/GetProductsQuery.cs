namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber=1, int? PageSize=10) : IQuery<List<GetProductResponse>>;
}
