namespace WebApplication1.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate requestDelegate, ILogger<RequestLoggingMiddleware> logger)
        {
            _requestDelegate=requestDelegate;
            _logger=logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.LogInformation("Request: {method} {url}. Remote IP {ip}",
                httpContext.Request.Method,
                httpContext.Request.Path,
                httpContext.Connection.RemoteIpAddress?.ToString()
            );

            await _requestDelegate(httpContext);
        }
    }
}
