namespace Ordering.Api;

public static  class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Register API services here
        // For example, you can add controllers, Swagger, etc.

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure the HTTP request pipeline for API services
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        return app;
    }
}
