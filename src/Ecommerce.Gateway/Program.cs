using Ecommerce.Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
await app.AddApplication();

await app.RunAsync();