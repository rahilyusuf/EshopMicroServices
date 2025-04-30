using BuildingBlocks.CQRS;

namespace Catalog.Api.Products.GetProducts
{
    
    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductsQuery,List<GetProductResponse>>
    {
        public async Task<List<GetProductResponse>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle Called with {@Query}:", query);
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
           
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
