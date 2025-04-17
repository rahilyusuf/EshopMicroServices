using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS;
//Icommand defines contract for all command types 
//Unit is from MediatR and represents a void return type (like Task<void> in async methods).
public interface ICommand : ICommand<Unit>
    {
    }

    //Generic version allows for commands that produce response 
    public interface ICommand<out TResponse>: IRequest<TResponse>
    {
    }
 
