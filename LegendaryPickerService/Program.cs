using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
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

// Enums, such as a moved card's kind and piles, are written as camelCase names, as in the box files.
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)));

builder.Services.AddSingleton(BoxCatalog.Load(BoxCatalog.DefaultDirectory));
builder.Services.AddSingleton<SetupGenerator>();
builder.Services.AddSingleton<SetupCodes>();
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

// The boxes a player can include in a setup, base games first, then by release month (#106). A base game supplies setup rules; an expansion adds cards.
// Each names its ruleset, so the app can offer a choice of rule edition when the ticked boxes follow more than one (#240).
app.MapGet("/api/boxes", (BoxCatalog catalog) =>
    Results.Ok(catalog.Boxes.OrderByDescending(box => box.IsBaseGame).ThenBy(box => box.About.Released.Value, StringComparer.Ordinal).Select(box => new
    {
        box.Id, box.Name, BaseGame = box.IsBaseGame, box.Ruleset, box.About.FirstPublished, Description = box.About.Description.Value,
    })));

// Every card a player can choose to be in the game (#337): each Scheme, Mastermind, Villain Group, Henchman Group and Hero
// of every box, ticked or not, with its id, name, kind (the kind part of its id) and the boxes that hold it, the declaring
// box first. The list is about 800 cards, so the app loads it once.
app.MapGet("/api/cards", (BoxCatalog catalog) =>
    Results.Ok(catalog.Boxes
        .SelectMany(box => box.Schemes.Cast<ICard>().Concat(box.Masterminds).Concat(box.VillainGroups).Concat(box.HenchmanGroups).Concat(box.Heroes))
        .Select(card => new
        {
            card.Id,
            card.Name,
            Kind = card.Id.Split('_')[1],
            Boxes = catalog.Boxes.Where(box => box.Holds(card.Id)).Select(box => box.Id),
        })));

// The band names by rating, 1 to 5, so the picker lists the bands the scorers use instead of keeping its own copy (#271).
app.MapGet("/api/ratings", () => Results.Ok(new
{
    Difficulty = DifficultyTuning.Default.BandNames,
    Complexity = ComplexityTuning.Default.BandNames,
}));

// The rule editions a player can choose with rules=, by their names in the query string (#240).
var editions = new Dictionary<string, Ruleset>(StringComparer.Ordinal)
{
    ["first-edition"] = Ruleset.FirstEdition,
    ["villains"] = Ruleset.Villainous,
    ["revised"] = Ruleset.Revised,
};

// players is read as text so a non-number gets the same ProblemDetails as an out-of-range count.
// boxes is a comma-separated list of box ids; without it a setup uses the core box alone. epic=true plays a drawn
// Mastermind that has an Epic side on that side (#185). rules names the rule edition the whole setup follows; without
// it the boxes decide (D-editions, #240). include is a comma-separated list of the ids of cards the player chose to be in the
// game, of any kind (#337); a box that holds one is included even when boxes leaves it out.
// NoEligibleScheme and ChosenCardsConflict are valid answers, not errors, so they are a 200 like a setup.
app.MapGet("/api/setup", (string? players, string? boxes, bool? epic, string? rules, string? difficulty, string? complexity, string? include, HttpResponse response, SetupGenerator generator, IRandomSource random, BoxCatalog catalog, SetupCodes codes) =>
{
    // "Generate another" must always draw a fresh setup.
    response.Headers.CacheControl = "no-store";

    if (!int.TryParse(players, out var count) || count is < BoxCatalog.MinPlayers or > BoxCatalog.MaxPlayers)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["players"] = [$"players must be a whole number from {BoxCatalog.MinPlayers} to {BoxCatalog.MaxPlayers}."],
        });
    }

    Ruleset? edition = null;
    if (!string.IsNullOrEmpty(rules))
    {
        if (!editions.TryGetValue(rules, out var named))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["rules"] = [$"rules must be one of {string.Join(", ", editions.Keys)}."],
            });
        }

        edition = named;
    }

    // difficulty is the rating the draw leans toward, 1 to 5; without it the draw is plain (#267).
    int? target = null;
    if (!string.IsNullOrEmpty(difficulty))
    {
        if (!int.TryParse(difficulty, out var wanted) || wanted is < 1 or > 5)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["difficulty"] = ["difficulty must be a whole number from 1 to 5."],
            });
        }

        target = wanted;
    }

    // complexity is the rating of rules to learn the draw leans toward, 1 to 5, with the same rules as difficulty (#270).
    int? complexityTarget = null;
    if (!string.IsNullOrEmpty(complexity))
    {
        if (!int.TryParse(complexity, out var wantedComplexity) || wantedComplexity is < 1 or > 5)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["complexity"] = ["complexity must be a whole number from 1 to 5."],
            });
        }

        complexityTarget = wantedComplexity;
    }

    var chosen = (include ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    if (generator.CheckChosenCards(chosen) is { } wrongCard)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]> { ["include"] = [wrongCard] });
    }

    var included = generator.BoxesWith(
        (boxes ?? SetupGenerator.CoreBoxId).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet(StringComparer.Ordinal),
        chosen);
    if (generator.CheckBoxes(included, edition) is { } problem)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]> { ["boxes"] = [problem] });
    }

    var result = generator.Generate(count, included, random, epic ?? false, edition, target, complexityTarget, chosen);
    return Results.Ok(SetupResponse.From(result, catalog, target, complexityTarget, codes.For(result, edition, epic ?? false)));
}).RequireRateLimiting(SetupRateLimit);

// The most Heroes, Villain Groups and Henchman Groups a player can choose under Must include (#337) for a setup of these
// players, boxes, Epic choice, rule edition and chosen cards, read as /api/setup reads them. The app asks again whenever
// one changes, so it isn't rate-limited like a draw; it lays out no setup.
app.MapGet("/api/limits", (string? players, string? boxes, bool? epic, string? rules, string? include, SetupGenerator generator) =>
{
    if (!int.TryParse(players, out var count) || count is < BoxCatalog.MinPlayers or > BoxCatalog.MaxPlayers)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["players"] = [$"players must be a whole number from {BoxCatalog.MinPlayers} to {BoxCatalog.MaxPlayers}."],
        });
    }

    Ruleset? edition = null;
    if (!string.IsNullOrEmpty(rules))
    {
        if (!editions.TryGetValue(rules, out var named))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]> { ["rules"] = [$"rules must be one of {string.Join(", ", editions.Keys)}."] });
        }

        edition = named;
    }

    var chosen = (include ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    if (generator.CheckChosenCards(chosen) is { } wrongCard)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]> { ["include"] = [wrongCard] });
    }

    var included = generator.BoxesWith(
        (boxes ?? SetupGenerator.CoreBoxId).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet(StringComparer.Ordinal),
        chosen);
    if (generator.CheckBoxes(included, edition) is { } problem)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]> { ["boxes"] = [problem] });
    }

    return Results.Ok(generator.Limits(count, included, edition, chosen, epic ?? false));
});

// The setup a setup code names (#335, #340), with its ratings, so the app can reload a pasted code or a link and a script
// can read a code's setup without the browser. A code keeps no difficulty or complexity target: its cards are the ones
// the draw for them gave, so the reload shows their ratings without a target note. A code that isn't one is a 400; one the
// current data can no longer reproduce is a 409, whose detail says so.
app.MapGet("/api/setup/{code}", (string code, HttpResponse response, BoxCatalog catalog, SetupCodes codes) =>
{
    response.Headers.CacheControl = "no-store";
    return codes.Load(code) switch
    {
        SetupCodes.Loaded loaded => Results.Ok(SetupResponse.From(loaded.Setup, catalog, code: loaded.Code)),
        SetupCodes.Invalid invalid => Results.ValidationProblem(new Dictionary<string, string[]> { ["code"] = [invalid.Message] }),
        SetupCodes.Outdated outdated => Results.Problem(outdated.Message, statusCode: StatusCodes.Status409Conflict, title: "Setup code out of date"),
        var other => throw new InvalidOperationException($"Unknown reload {other}."),
    };
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
