using Chicago311Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// On Railway, listen on all interfaces using the PORT it provides.
// Locally PORT is not set, so launchSettings.json (http://localhost:5181) is used.
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

// Allowed frontend origins: localhost for development, plus any extra origins
// from the AllowedOrigins setting (comma-separated, e.g. the deployed Angular URL).
var allowedOrigins = new List<string> { "http://localhost:4200" };
allowedOrigins.AddRange(
    (builder.Configuration["AllowedOrigins"] ?? "")
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(origin => origin.TrimEnd('/'))
);

// Allow the Angular frontend to call this API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy
            .WithOrigins(allowedOrigins.ToArray())
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add controllers.
builder.Services.AddControllers();

// Connect to MySQL.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// In production on Railway, read environment variables and build connection string
var mysqlHost = Environment.GetEnvironmentVariable("MYSQLHOST");
if (!string.IsNullOrEmpty(mysqlHost))
{
    var mysqlPort = Environment.GetEnvironmentVariable("MYSQLPORT");
    var mysqlDatabase = Environment.GetEnvironmentVariable("MYSQLDATABASE");
    var mysqlUser = Environment.GetEnvironmentVariable("MYSQLUSER");
    var mysqlPassword = Environment.GetEnvironmentVariable("MYSQLPASSWORD");

    connectionString = $"Server={mysqlHost};Port={mysqlPort};Database={mysqlDatabase};User={mysqlUser};Password={mysqlPassword};ConnectionTimeout=10;";
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

// Add Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS.
app.UseCors("Angular");

// Enable Swagger.
app.UseSwagger();
app.UseSwaggerUI();

// Enable controller endpoints.
app.MapControllers();

app.Run();
