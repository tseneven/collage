using trpo6.Infrastructure.MSSQL;
using trpo6.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<WebApiDatabaseContext>();
builder.Services.AddScoped<EntityStorage>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
