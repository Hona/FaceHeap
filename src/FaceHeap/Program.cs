using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

[assembly: VogenDefaults(customizations: Customizations.AddFactoryMethodForGuids)]

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints(options =>
{
    options.SourceGeneratorDiscoveredTypes.AddRange(FaceHeap.DiscoveredTypes.All);
});
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.SwaggerDocument();

builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});

var app = builder.Build();

app.UseRateLimiter();

app.UseFastEndpoints(config =>
{
    config.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
});
app.UseSwaggerGen();

// Serve the UI & fallback to index.html
// Could do this with Azure Static Web Apps etc, but this is simple.
app.UseRewriter(new RewriteOptions().AddRewrite("^$", "index.html", skipRemainingRules: true));
app.UseStaticFiles();

// show 404 page
app.UseStatusCodePagesWithReExecute("/error", "?code={0}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.Run();

public partial class Program;
