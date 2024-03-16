namespace DotNetMiddlewares;

// convention middleware
internal sealed class RegisterEntityIdentifier(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, AppDbContext appDbContext)
    {
        appDbContext.IdentityTrackers.Add(new IdentityTracker(context.TraceIdentifier));
        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}