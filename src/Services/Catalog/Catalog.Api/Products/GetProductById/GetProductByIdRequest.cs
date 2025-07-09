namespace Catalog.Api.Products.GetProductById
{
    public record GetProductByIdRequest(Guid Id) : IQuery<GetProductByIdResponse>;
   

}
