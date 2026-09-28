using System.Net;
using System.Net.Sockets;
using System.Threading.RateLimiting;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;
using Microsoft.AspNetCore.HttpOverrides;
using IPNetwork = System.Net.IPNetwork;

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

// On App Service every request reaches the app from the platform front end, which appends the
// caller's address to X-Forwarded-For. Only that hop (link-local, seen as 169.254.129.1 on the
// deployed app) is trusted, so a client that sends its own X-Forwarded-For can't pick its rate-limit
// bucket. App Service also turns on the framework's forwarded headers by default
// (ASPNETCORE_FORWARDEDHEADERS_ENABLED) with every source trusted; configuring these options replaces
// that trust list, because both middlewares read the same options.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownProxies.Clear();
    options.KnownIPNetworks.Clear();
    options.KnownIPNetworks.Add(IPNetwork.Parse("169.254.0.0/16"));
});

// The API is public with no sign-in, so each client IP gets a fixed number of setups a minute.
// An IPv6 client can rotate addresses within its /64, so the whole /64 shares one bucket.
// Health stays unlimited: the frontend pings it on load to wake the app.
const string SetupRateLimit = "setup";
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy(SetupRateLimit, context => RateLimitPartition.GetFixedWindowLimiter(
        ClientBucket(context.Connection.RemoteIpAddress),
        _ => new FixedWindowRateLimiterOptions { PermitLimit = 30, Window = TimeSpan.FromMinutes(1) }));
});

builder.Services.AddSingleton(BoxCatalog.Load(BoxCatalog.DefaultDirectory));
builder.Services.AddSingleton<SetupGenerator>();
builder.Services.AddSingleton<IRandomSource, SharedRandomSource>();

var app = builder.Build();

// Forwarded headers run first, so the rate limiter partitions on the caller's address.
app.UseForwardedHeaders();
// CORS runs next so a 429 still carries Access-Control-Allow-Origin and the browser app can read it.
app.UseCors();
app.UseRateLimiter();

// HEAD too, because UptimeRobot's free tier checks with HEAD; its pings keep the F1 app from idling.
// HEAD gets no body from the handler itself rather than relying on the server to drop one.
app.MapMethods("/api/health", ["GET", "HEAD"], (HttpRequest request) =>
    HttpMethods.IsHead(request.Method) ? Results.Ok() : Results.Ok(new { status = "ok" }));

// The boxes a player can include in a setup. A base game supplies setup rules; an expansion adds cards.
app.MapGet("/api/boxes", (BoxCatalog catalog) =>
    Results.Ok(catalog.Boxes.Select(box => new { box.Id, box.Name, BaseGame = box.IsBaseGame })));

// players is read as text so a non-number gets the same ProblemDetails as an out-of-range count.
// boxes is a comma-separated list of box ids; without it a setup uses the core box alone.
// NoEligibleScheme is a valid answer, not an error, so it is a 200 like a setup.
const int MinPlayers = 1;
const int MaxPlayers = 5;
app.MapGet("/api/setup", (string? players, string? boxes, HttpResponse response, SetupGenerator generator, IRandomSource random, BoxCatalog catalog) =>
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

    var included = (boxes ?? SetupGenerator.CoreBoxId)
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .ToHashSet(StringComparer.Ordinal);
    if (generator.CheckBoxes(included) is { } problem)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]> { ["boxes"] = [problem] });
    }

    return Results.Ok(SetupResponse.From(generator.Generate(count, included, random), catalog));
}).RequireRateLimiting(SetupRateLimit);

app.Run();

static string ClientBucket(IPAddress? address)
{
    if (address is null)
    {
        return "unknown";
    }

    if (address.IsIPv4MappedToIPv6)
    {
        return address.MapToIPv4().ToString();
    }

    return address.AddressFamily == AddressFamily.InterNetworkV6
        ? new IPNetwork(address, 64).BaseAddress + "/64"
        : address.ToString();
}

// Lets the tests host the app through WebApplicationFactory<Program>.
public partial class Program;
