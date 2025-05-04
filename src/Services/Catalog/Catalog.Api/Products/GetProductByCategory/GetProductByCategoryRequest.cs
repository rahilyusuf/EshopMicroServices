namespace Catalog.Api.Products.GetProductByCategory
{
    public record GetProductByCategoryRequest(string Category) :
        IQuery<List<GetProductByCategoryResponse>>;

}
