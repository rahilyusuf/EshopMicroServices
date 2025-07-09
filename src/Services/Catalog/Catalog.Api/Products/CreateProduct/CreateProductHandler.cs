
namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageUrl, decimal Price)
            : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");
            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Category is required.");
            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .WithMessage("ImageUrl is required.");
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");
        }
    }
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
