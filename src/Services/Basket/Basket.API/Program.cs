using Basket.API.Data;
using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var assembly = typeof(Program).Assembly;
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            builder.Services.AddMarten(options =>
            {
                options.Connection(builder.Configuration.GetConnectionString("Database")!);
                options.Schema.For<ShoppingCart>().Identity(x => x.UserName);//here we are usingusername as identifier
            }).UseLightweightSessions();

            //Instead of manually adding we will register the repository and cache decorator with scrutor library

            builder.Services.AddScoped<IBasketRepository,BasketRepository>();
            builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

            //builder.Services.AddScoped<IBasketRepository>(provider =>
            //{
            //    var basketRepository = provider.GetRequiredService<BasketRepository>(); // the actual implementation
            //    var cache = provider.GetRequiredService<IDistributedCache>(); // caching service (e.g., Redis)

            //    return new CachedBasketRepository(basketRepository, cache); // decorated repository
            //});
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
                options.InstanceName = "BasketInstance";
            });

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();
            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
                .AddRedis(builder.Configuration.GetConnectionString("Redis")!);




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseExceptionHandler(options => { });


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();

            

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            app.Run();
        }
    }
}
