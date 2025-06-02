
namespace Basket.API.Basket.DeleteBasket
{
    [Route("api/[controller]")]
    [ApiController]
    

    public class DeleteBasketController : Controller
    {

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        
        public async Task<IActionResult> DeleteBasket(
            [FromRoute] string id,
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new DeleteBasketCommand(id), cancellationToken);
            var response = result.Adapt<DeleteProductResponse>();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound();
        }
    }
}
