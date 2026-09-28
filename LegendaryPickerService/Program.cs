using System.Threading.RateLimiting;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy
        .WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// The API is public with no sign-in, so each client IP gets a fixed number of setups a minute.
// Health stays unlimited: the frontend pings it on load to wake the app.
const string SetupRateLimit = "setup";
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy(SetupRateLimit, context => RateLimitPartition.GetFixedWindowLimiter(
        context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
        _ => new FixedWindowRateLimiterOptions { PermitLimit = 30, Window = TimeSpan.FromMinutes(1) }));
});

builder.Services.AddSingleton(BoxCatalog.Load(BoxCatalog.DefaultDirectory));
builder.Services.AddSingleton<SetupGenerator>();
builder.Services.AddSingleton<IRandomSource, SharedRandomSource>();

var app = builder.Build();

// CORS runs first so a 429 still carries Access-Control-Allow-Origin and the browser app can read it.
app.UseCors();
app.UseRateLimiter();

app.MapGet("/api/health", () => Results.Ok(new { status = "ok" }));

// players is read as text so a non-number gets the same ProblemDetails as an out-of-range count.
// NoEligibleScheme is a valid answer, not an error, so it is a 200 like a setup.
const int MinPlayers = 1;
const int MaxPlayers = 5;
app.MapGet("/api/setup", (string? players, HttpResponse response, SetupGenerator generator, IRandomSource random) =>
{
    // "Generate another" must always draw a fresh setup.
    response.Headers.CacheControl = "no-store";

    if (!int.TryParse(players, out var count) || count is < MinPlayers or > MaxPlayers)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["players"] = [$"players must be a whole number from {MinPlayers} to {MaxPlayers}."],
        });
    }

    return Results.Ok(SetupResponse.From(generator.Generate(count, random)));
}).RequireRateLimiting(SetupRateLimit);

app.Run();

// Lets the tests host the app through WebApplicationFactory<Program>.
public partial class Program;
