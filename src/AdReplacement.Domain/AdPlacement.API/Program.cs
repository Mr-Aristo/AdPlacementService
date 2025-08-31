

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((ctx, cfg) => cfg
.ReadFrom.Configuration(ctx.Configuration)
.Enrich.FromLogContext()
.Enrich.WithEnvironmentName()
.Enrich.WithMachineName());


// Services
builder.Services.AddApplicationServices();


builder.Services.AddSingleton<IPlacementIndexAccessor, PlacementIndexAccessor>();
builder.Services.AddSingleton<IPlacementQuery, InMemoryPlacementQuery>();
builder.Services.AddSingleton<IPlacementLoader, InMemoryPlacementsLoader>();


builder.Services.AddHealthChecks()
.AddCheck("self", () => HealthCheckResult.Healthy())
.AddCheck<IndexLoadedHealthCheck>("index_loaded");


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}



// Routes
app.MapPost("/api/platforms/upload", async (HttpRequest request, ISender sender, ILoggerFactory lf) =>
{
    string content;
    if (request.HasFormContentType)
    {
        var form = await request.ReadFormAsync();
        var file = form.Files.GetFile("file");
        if (file is null) return Results.BadRequest(new { error = "multipart/form-data requires 'file'" });
        using var sr = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
        content = await sr.ReadToEndAsync();
    }
    else
    {
        using var sr = new StreamReader(request.Body, Encoding.UTF8);
        content = await sr.ReadToEndAsync();
    }


    if (string.IsNullOrWhiteSpace(content)) return Results.BadRequest(new { error = "empty content" });


    var result = await sender.Send(new UploadPlacementsCommand(content));
    return Results.Ok(result);
})
.Accepts<IFormFile>("multipart/form-data")
.Produces(200)
.WithTags("Platforms");


app.MapGet("/api/platforms", async (string location, ISender sender) =>
{
    var res = await sender.Send(new GetPlatformsByLocationQuery(location));
    return Results.Ok(res);
})
.Produces(200)
.WithTags("Platforms");


app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = r => r.Name == "self",
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
