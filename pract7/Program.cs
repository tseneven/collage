using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using pract7.Application.DBContext;
using pract7.Application.Models;
using pract7.Application.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key = builder.Configuration["Jwt:Key"]
          ?? throw new Exception("Не задан секретный ключ JWT");
if (Encoding.UTF8.GetByteCount(key) < 32)
    throw new Exception("Ключ JWT слишком короткий");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "ExampleApi",
            ValidateAudience = true,
            ValidAudience = "ExampleApiClient",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)
            ),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<KuznetsovTaskDbContext>();
builder.Services.AddScoped<UserSerivce>();
builder.Services.AddScoped<TaskService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userService = scope.ServiceProvider.GetRequiredService<UserSerivce>();
    userService.CreateAdmin();
}

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
