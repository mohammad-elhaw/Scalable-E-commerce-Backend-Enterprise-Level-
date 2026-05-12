using Ecommerce.Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();

var app = builder.Build();
await app.AddApplication();

await app.RunAsync();