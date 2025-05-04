namespace Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductCommand
    (
        Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageUrl,
        decimal Price
    ) : ICommand<UpdateProductResult>;
    public record UpdateProductResult
    (
        bool IsSuccess
    );  
internal class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) :
            ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler.Handle Called with {@Command}:", request);
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException($"Product with the specified ID:{request.Id} was not found.");
            }

            // Simulate product update logic
            product.Name = request.Name;
            product.Category = request.Category;
            product.Description = request.Description;
            product.ImageUrl = request.ImageUrl;
            product.Price = request.Price;
            
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }

    }
}
