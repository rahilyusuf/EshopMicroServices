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
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor( x => x.Id ).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.").Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
            RuleFor(x => x.Price).GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

        }
    }
    internal class UpdateProductHandler(IDocumentSession session) :
            ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
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
