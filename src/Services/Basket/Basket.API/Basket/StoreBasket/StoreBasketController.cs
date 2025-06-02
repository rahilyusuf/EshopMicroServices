using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Basket.StoreBasket
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class StoreBasketController : Controller
    {
       
        // GET: StoreBasketController
        [HttpPost]
        public async Task<IActionResult> StoreBasket(
            [FromBody] StoreBasketRequest request,
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var command = request.Adapt<StoreBasketCommand>();
            var result = await sender.Send(command, cancellationToken);
            var response = result.Adapt<StoreBasketResponse>();
            return Created($"/basket/{response.UserName}", response);
        }

    }
}
