using Basket.API.Data;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName): ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("UserName is required");
        }
    }
    public class DeleteBasketHandler(IBasketRepository repository) :ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            // Logic to delete the basket
            //TODO : delete operation from db cache 
            //session.Delete<Product>(command.Id);
            await repository.DeleteBasket(command.UserName, cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
  
}
