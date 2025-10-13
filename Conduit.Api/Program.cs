using Conduit.Modules.Users.Application.Commands.Register;
using Conduit.Modules.Users.Infrastructure;
using Conduit.Modules.Users.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//MAIN THING:
//CRUD Angular + NET Core + Entity Framework Core + MySql

//SIDE COURSES
//Build an app with ASPNET Core and Angular from scratch
//Getting Started with .NET Core Clean Architecture
//.NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture



var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
    };
});
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseAuthorization();
app.UseAuthentication();

app.UseCors("AllowAngularDev");
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment()) 
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();


/* ReadUpOnAndConsider
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
*/

/*
try
{
    var userCount = await dbContext.Users.CountAsync();
    Console.WriteLine($"Users table has {userCount} rows.");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to query database: {ex.Message}");
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    if (dbContext.Database.CanConnect())
    {
        Console.WriteLine("Database connection OK!");
    }
    else
    {
        Console.WriteLine("Cannot connect to database!");
    }

}
*/