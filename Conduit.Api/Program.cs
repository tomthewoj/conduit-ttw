using Conduit.Infra.Data.Context;
using Conduit.Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

var provider = config["DatabaseProvider"];

builder.Services.AddDbContext<ConduitDbContext>(options =>
{
    if (provider == "Postgres")
    {
        options.UseNpgsql(config.GetConnectionString("Postgres"), b => b.MigrationsAssembly("Conduit.Infra.Data"));
    }
    else if (provider == "SqlServer")
    {
        options.UseSqlServer(config.GetConnectionString("SqlServer"), b => b.MigrationsAssembly("Conduit.Infra.Data"));
    }
});
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy => policy 
            .SetIsOriginAllowed(origin => origin.StartsWith("https://localhost"))
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });

    // Add JWT authentication to Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
var port = Environment.GetEnvironmentVariable("PORT") ?? "7255";
var useHttps = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PORT")); //only dev has this

builder.WebHost.ConfigureKestrel(options =>
{
    if (useHttps) options.ListenAnyIP(int.Parse(port), listenOptions => listenOptions.UseHttps());
    else options.ListenAnyIP(int.Parse(port));
});

var app = builder.Build();

/*
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ConduitDbContext>();
    db.Database.Migrate();
}
*/

app.UseCors("AllowAngularDev");
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment()) 
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles(); //for frontend
app.UseStaticFiles(); //for frontend
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