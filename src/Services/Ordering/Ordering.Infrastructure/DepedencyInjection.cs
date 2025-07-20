
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class DepedencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        // Register infrastructure services here
        // For example, you can add Entity Framework, Dapper, etc.

        // services.AddDbContext<OrderingContext>(options =>
        //     options.UseSqlServer("YourConnectionString"));
        // services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}
