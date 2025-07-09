using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add gRPC services to the container
builder.Services.AddGrpc();

// Register DbContext using SQLite provider
builder.Services.AddDbContext<DiscountContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Database")!));

// Apply database migrations at startup (extension method)
//builder.Services.UseMigration();

var app = builder.Build();


// Configure the gRPC request pipeline
app.UseMigration();
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Use a gRPC client to communicate with gRPC endpoints.");

app.Run();
