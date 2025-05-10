
namespace Catalog.Api.Products.GetProducts
{
    
    internal class GetProductsQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsQuery,List<GetProductResponse>>
    {
        public async Task<List<GetProductResponse>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1 ,query.PageSize ?? 10, cancellationToken);
           
            // Map Product → GetProductResponse
            var response = products.Select(p => new GetProductResponse(
                p.Id,
                p.Name,
                p.Category,
                p.Description, 
                p.ImageUrl,
                p.Price
            )).ToList();

            return response;
        }
    }
}
