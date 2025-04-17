namespace Catalog.Api.Products.CreateProduct
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateProductController : ControllerBase
    {
        private readonly ISender _sender;

        public CreateProductController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost]
        [ProducesResponseType(typeof(CreateProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]

        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        { 
            var command = request.Adapt<CreateProductCommand>();
            var result = await _sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return CreatedAtAction(nameof(CreateProduct), new { id = result.Id }, response);
        }
    }
}
