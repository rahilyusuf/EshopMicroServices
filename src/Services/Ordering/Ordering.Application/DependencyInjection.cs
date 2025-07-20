
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjection 
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register application services here
        // For example, you can add MediatR, AutoMapper, etc.

        // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        // services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        return services;
    }
}
