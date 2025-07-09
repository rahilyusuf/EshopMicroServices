using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProductById
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProductByIdController : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetProductByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> GetProductById(
            [FromRoute] Guid id,
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var query = new GetProductByIdRequest(id);
            var result = await sender.Send(query, cancellationToken);
            var response = result.Adapt<GetProductByIdResponse>();
            return Ok(response);
        }
    }
}
