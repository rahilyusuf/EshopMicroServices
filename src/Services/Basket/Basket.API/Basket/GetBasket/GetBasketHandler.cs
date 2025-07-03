
using Basket.API.Data;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    
        //incoming request comming from controler here will look for IquerrHandler that implmenets
        public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
        {
            public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
            {
            // Simulate a delay to mimic async behavior 
            // Task from db
            //return new GetBasketResult(new ShoppingCart("swn"));
            var basket = await repository.GetBasket(query.UserName);
            return new GetBasketResult(basket);

            }
        }
    }
