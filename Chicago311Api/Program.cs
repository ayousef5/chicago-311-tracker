var builder = WebApplication.CreateBuilder(args);

// Register HttpClient for the Chicago 311 API.
builder.Services.AddHttpClient();

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
