using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Rewrite;

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

var app = builder.Build();

app.UseFastEndpoints(config =>
{
    config.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
});
app.UseSwaggerGen();

// Serve the UI & fallback to index.html
// Could do this with Azure Static Web Apps etc, but this is simple.
app.UseRewriter(new RewriteOptions().AddRewrite("^$", "index.min.html", skipRemainingRules: true));
app.MapStaticAssets();

// show 404 page
app.UseStatusCodePagesWithReExecute("/error", "?code={0}");

app.Run();
