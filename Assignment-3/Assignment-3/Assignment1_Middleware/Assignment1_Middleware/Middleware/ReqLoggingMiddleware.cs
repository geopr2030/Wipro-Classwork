using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Assignment1_Middleware.Middleware
{
    public class ReqLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ReqLoggingMiddleware> _logger;

        public ReqLoggingMiddleware(RequestDelegate next, ILogger<ReqLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Request: {method} {url}",
                context.Request.Method,
                context.Request.Path);

            await _next(context);

            _logger.LogInformation("Response Status Code: {statusCode}",
                context.Response.StatusCode);
        }
    }
}