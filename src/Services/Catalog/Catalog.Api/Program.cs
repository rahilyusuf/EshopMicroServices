using MapsterMapper;
using Microsoft.OpenApi.Models;


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
            builder.Services.AddSwaggerGen(
                );

            


            builder.Services.AddMediatR(config=>
                config.RegisterServicesFromAssembly(typeof(Program).Assembly)
                );

            builder.Services.AddScoped(typeof(IMapper), provider=>TypeAdapterConfig.GlobalSettings);

            var app = builder.Build();
            //Configure the HTTP request pipeline

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
