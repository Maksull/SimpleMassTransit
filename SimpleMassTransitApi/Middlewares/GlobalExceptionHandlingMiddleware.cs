using Microsoft.AspNetCore.Mvc;

namespace SimpleMassTransitApi.Middlewares
{
    public sealed class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unhandled exception occurred. Exception: {Exception}", ex);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                ProblemDetails problemDetails = new()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = ex.InnerException?.Message,
                };


                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
