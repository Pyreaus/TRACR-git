using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
public class GlobalExceptionHandler : IExceptionHandler                              //ASP.NET Core 8 required
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;
    public async ValueTask<bool> TryHandleAsync(HttpContext ctx, Exception exc, CancellationToken ctk = default)
    {
        _logger.LogError(exc, "Exception: {Message}", exc.Message);
        
        var problemInfo = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Unexpected Error",
            Type = @"https://datatracker.itef.org/doc/html/rfc7231#section-6.6.1"
        };

        ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await ctx.Response.WriteAsJsonAsync(problemInfo, ctk);

        return true;
    }
}



// builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// builder.Services.AddProblemDetails();

// app.UseExceptionHandler();




public interface IExceptionHandler {}