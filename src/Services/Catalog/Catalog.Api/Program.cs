
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Catalog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //add Services to the container


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var assembly = typeof(Program).Assembly;
            builder.Services.AddMediatR(config => {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            }
                );
            builder.Services.AddValidatorsFromAssembly(assembly);

            builder.Services.AddScoped(typeof(IMapper), provider=>TypeAdapterConfig.GlobalSettings);

           



            builder.Services.AddMarten(opts =>
            {
                opts.Connection(builder.Configuration.GetConnectionString("Database")!);
                //Console.WriteLine($"Connection string: {connStr}");
                //if (string.IsNullOrWhiteSpace(connStr))
                //    throw new InvalidOperationException("Connection string 'Database' is not found or is empty.");

                //opts.Connection(connStr);
            }).UseLightweightSessions();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.InitializeMartenWith<CatalogInitialData>();
            }

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

            var app = builder.Build();


            //Configure the HTTP request pipeline

            app.UseExceptionHandler(options => { });

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }
                );



            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
