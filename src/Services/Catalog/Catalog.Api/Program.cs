
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
                }
                );
            builder.Services.AddValidatorsFromAssembly(assembly);

            builder.Services.AddScoped(typeof(IMapper), provider=>TypeAdapterConfig.GlobalSettings);

            builder.Services.AddMarten(opts =>
            {
                opts.Connection(builder.Configuration.GetConnectionString("Database"));

            }).UseLightweightSessions();

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            var app = builder.Build();


            //Configure the HTTP request pipeline

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


            app.Run();
        }
    }
}
