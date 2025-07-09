
namespace Catalog.Api.Products.GetProductById
{
    internal class GetProductByIdQueryHandler (IDocumentSession session):
        IQueryHandler<GetProductByIdRequest, GetProductByIdResponse>
    {
        public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest query, CancellationToken cancellationToken)
        {
            
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException($"Product with ID {query.Id} not found.");
            }
            // Map Product → GetProductByIdResponse
            var response = new GetProductByIdResponse(
                product.Id,
                product.Name,
                product.Category,
                product.Description,
                product.ImageUrl,
                product.Price
            );
            return response;

        }
    }
}
