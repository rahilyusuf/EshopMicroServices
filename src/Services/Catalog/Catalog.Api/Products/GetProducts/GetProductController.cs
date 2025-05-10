using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProducts
{
        [Route("api/[controller]")]
        [ApiController]
        public class GetProductController : ControllerBase
        {
            private readonly ISender _sender;
            public GetProductController(ISender sender)
            {
                _sender = sender;
            }
            [HttpGet]
            public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery request, CancellationToken cancellationToken)
            {
                var query = request.Adapt<GetProductsQuery>();
                var result = await _sender.Send(query, cancellationToken);
            var response = result.Adapt<GetProductResponse>();
            return Ok(result);
            }
        }
    }
