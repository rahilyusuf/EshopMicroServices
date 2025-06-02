using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProductByCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProductByCategoryController : ControllerBase
    {
        [HttpGet("{category}")]
        [ProducesResponseType(typeof(GetProductByCategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> GetProductByCategory(
            [FromRoute] string category,
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var query = new GetProductByCategoryRequest(category);
            var result = await sender.Send(query, cancellationToken);
            var response = result.Adapt<GetProductByCategoryResponse>();
            return Ok(response);
        }
    }
}
