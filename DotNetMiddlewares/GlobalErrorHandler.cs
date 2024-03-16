using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace DotNetMiddlewares;

// Factory based middleware
public class GlobalErrorHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
			await next(context);
		}
		catch
		{
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			ProblemDetails problem = new()
			{
				Status = (int)HttpStatusCode.InternalServerError,
				Type = "Server Error",
				Title = "Server Error",
				Detail = "Server Error"
            };

			string json = JsonSerializer.Serialize(problem);

			await context.Response.WriteAsJsonAsync(json);
			context.Response.ContentType = "application/json";
		}
    }
}
