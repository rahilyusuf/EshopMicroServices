using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior <TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest,TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull,IRequest<TResponse>
        where TResponse : notnull

    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Log the request here
            logger.LogInformation("[Start] Handling request = {Request} -Response ={Request} - RequestData ={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);
            var timer = new Stopwatch();
            timer.Start ();
            
            var response = await next();
            timer.Stop();
           
            var timeTaken = timer.Elapsed;
            // Log the response here
            if(timeTaken.Seconds > 3)
                logger.LogWarning("[Performance] The {Request} took {TimeTaken}",
                    typeof(TRequest).Name, timeTaken.Seconds);

            logger.LogInformation("[End] Handled request = {Request} -Response ={Request} - RequestData ={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);
            return response;
        }
    }
}
