
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(policyBuilder => policyBuilder.With(c => c.HttpContext.Request.Path.StartsWithSegments("/")).Expire(TimeSpan.FromMinutes(10)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseOutputCache();
app.MapReverseProxy();


app.Run();