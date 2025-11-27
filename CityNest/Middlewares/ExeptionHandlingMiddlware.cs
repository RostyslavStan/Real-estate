namespace CityNest.Middlewares
{
    public class ExeptionHandlingMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionHandlingMiddlware> _logger;
        public ExeptionHandlingMiddlware(RequestDelegate next, ILogger<ExeptionHandlingMiddlware> logger)
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
                _logger.LogError(ex, "Exeption occurred {Message}", ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync("Error");
            }

        }

    }
}
