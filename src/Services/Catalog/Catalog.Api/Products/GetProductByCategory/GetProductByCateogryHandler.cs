namespace Catalog.Api.Products.GetProductByCategory
{
    internal class GetProductByCateogryHandler(IDocumentSession session ) :
        IQueryHandler<GetProductByCategoryRequest, List<GetProductByCategoryResponse>>
    {
        public async Task<List<GetProductByCategoryResponse>> Handle(GetProductByCategoryRequest query, CancellationToken cancellationToken)
        {
            
            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync(cancellationToken);
            // Map Product → GetProductByCategoryResponse
            var response = products.Select(p => new GetProductByCategoryResponse(
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
