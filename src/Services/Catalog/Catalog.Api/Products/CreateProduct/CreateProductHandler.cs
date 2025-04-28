


namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageUrl, decimal Price)
            : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) :
            ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
            // Business Logic to create a product

            // Create a new product entity from Command Object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageUrl = command.ImageUrl,
                Price = command.Price
            };

            // Simulate saving to the database and generating an ID
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // Return the CreateProductResult result
            return new CreateProductResult(product.Id);
        }
    }
}
