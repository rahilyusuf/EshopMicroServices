

using MediatR;

namespace BuildingBlocks.CQRS;


public interface IQuerry<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
    
}
