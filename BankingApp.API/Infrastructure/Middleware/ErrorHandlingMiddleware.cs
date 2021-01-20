using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BankingApp.API.Infrastructure.Middleware {
    public class ErrorHandlingMiddleware {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory logger) {
            _next = next;
            _logger = logger.CreateLogger<ErrorHandlingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            }
            catch( Exception ex ) {
                _logger.LogError(ex, "An Exception Has Occured!");
                await HandlerExceptionAsync(context, ex);
            }
        }

        public static Task HandlerExceptionAsync(HttpContext context, Exception ex) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails() {
                StatusCode = context.Response.StatusCode,
                Message = "Oops! Something went wrong. We'll get onto it as soon as possible. You can contact us at xxxx",
                }.ToString());

        }
    }
}
