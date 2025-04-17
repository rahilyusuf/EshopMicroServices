using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    public interface IQuerry<in TQuerry, TResponse> : IRequestHandler<TQuerry,TResponse>
        where TQuerry : IQuerry<TResponse>
        where TResponse : notnull
    {

    }
}
