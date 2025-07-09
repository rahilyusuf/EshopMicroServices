using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    // The 'CachedBasketRepository' class is implementing the 'IBasketRepository' interface.
    // This class receives (injects) an instance of 'IBasketRepository' via constructor injection.
    // This is an example of both:
    // 1. Proxy Pattern: Because it controls access to another object (the original repository),
    //    and can potentially add extra behavior (like caching, logging, etc.) in the future.
    // 2. Decorator Pattern: Because it wraps the original 'IBasketRepository' implementation,
    //    allowing new behavior to be added dynamically without modifying the original class.
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        
        // The 'GetBasket' method delegates the call to the injected repository.
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
          
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                Console.WriteLine($"Returning basket from cache");
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }

            var basket = await repository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        // The 'StoreBasket' method delegates the call to the injected repository.
        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
             await repository.StoreBasket(basket, cancellationToken);
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket),cancellationToken);
            return basket;
        }
        // The 'DeleteBasket' method delegates the call to the injected repository.
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasket(userName,cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
            return true;

        }

    }
}
