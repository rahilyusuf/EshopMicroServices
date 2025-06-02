
namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketHandle
    {
        //incoming request comming from controler here will look for IquerrHandler that implmenets
        public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
        {
            public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
            {
                // Simulate a delay to mimic async behavior 
                // Task from db
                return new GetBasketResult(new ShoppingCart("swn"));
                
               
            }
        }
    }
}
