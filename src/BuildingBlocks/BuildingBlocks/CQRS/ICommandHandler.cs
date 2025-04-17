using MediatR;

namespace BuildingBlocks.CQRS
{
    //Icommand defines contract for all command types. iF you don't need a response, use Unit
    public interface ICommandHandler<in TCommand>
        : ICommandHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
    }

    //Generic version allows for commands that produce response
    public interface ICommandHandler<in TCommand, TResponse>
        : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
       
    }
}
