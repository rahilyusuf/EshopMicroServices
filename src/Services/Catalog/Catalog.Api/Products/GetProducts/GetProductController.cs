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
            public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
            {
                var query = new GetProductsQuery();
                var result = await _sender.Send(query, cancellationToken);
                return Ok(result);
            }
        }
    }
