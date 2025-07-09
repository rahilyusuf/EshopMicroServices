
using Basket.API.Data;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required");
        }
    }
    public class StoreBasketCommandHandler (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.Cart, cancellationToken); 

            //store basket using merten upsurt method -if exist =update, if not exist = insert
            await repository.StoreBasket(command.Cart, cancellationToken);

            //return result
            return new StoreBasketResult(command.Cart.UserName);
        }

        public async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
           // communicate with discount.grpc and calculate the latest price of products in  
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                if (coupon != null && coupon.Amount > 0)
                {
                    item.Price -= coupon.Amount; // Apply discount to the item price
                }
            }
        }

    }
}
