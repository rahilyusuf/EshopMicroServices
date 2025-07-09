using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.DeleteProduct
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteProductController : ControllerBase
    {
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteProduct(
            [FromRoute] Guid id,
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command, cancellationToken);
            var response = result.Adapt<DeleteProductResponse>();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound();
        }

        public record DeleteProductResponse(bool IsSuccess);
    }
}
