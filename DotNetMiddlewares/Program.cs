using DotNetMiddlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("db"));
builder.Services.AddTransient<GlobalErrorHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RegisterEntityIdentifier>();
app.UseMiddleware<GlobalErrorHandler>();

app.MapGet("/GetIdentity", (HttpContext context, AppDbContext AppDbContext) =>
{
    EntityEntry<IdentityTracker>? entityTrack = AppDbContext.IdentityTrackers.Local.FindEntry(context.TraceIdentifier);
    
    return entityTrack?.Entity;
}).WithName("GetIdentity")
.WithOpenApi();

app.Run();