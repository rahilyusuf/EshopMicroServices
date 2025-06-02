
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//using static Basket.API.Basket.GetBasket.GetBasketHandle;


namespace Basket.API.Basket.GetBasket
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetBasketController : ControllerBase
    {
        // GET: api/<GetBasketController>
        [HttpGet("{username}")]
        public async Task<IActionResult> GetBasket(string username, ISender sender, CancellationToken cancellationToken)
        {

            var result = await sender.Send(new GetBasketQuery(username));
            var response = result.Adapt<GetBasketResponse>();

            return Ok(response);
        }
       
     }
}
