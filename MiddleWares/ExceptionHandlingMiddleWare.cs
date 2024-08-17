using System.Text.Json;
namespace FITrack.MiddleWares
{
    public class ExceptionHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleWare> _logger;
        public ExceptionHandlingMiddleWare(RequestDelegate next , ILogger<ExceptionHandlingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError,
            };
            var result = JsonSerializer.Serialize(new ProblemDetails { 
                 Type = "Server error",
                 Title = "Server error",
                 Detail = "An internal server error"
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
