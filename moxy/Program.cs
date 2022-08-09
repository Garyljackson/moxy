
using Microsoft.AspNetCore.OutputCaching;
using moxy.CustomPolicies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(policyBuilder => policyBuilder
        .AddPolicy<CacheEverythingPolicy>()
        .With(c => c.HttpContext.Request.Path.StartsWithSegments(""))
        .Tag("*")
        .Expire(TimeSpan.FromMinutes(10)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapPost("/_/purge", async (IOutputCacheStore cache, CancellationToken ct) =>
{
    await cache.EvictByTagAsync("*", ct);
}).CacheOutput(policyBuilder => policyBuilder.NoCache());

app.UseOutputCache();
app.MapReverseProxy();

app.Run();