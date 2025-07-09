using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.UpdateProduct
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(UpdateProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")] 
    public class UpdateProductController : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(
            
            [FromBody] UpdateProductRequest request,
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command, cancellationToken);
            var response = result.Adapt<UpdateProductResponse>();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound();
        }

    }
}
