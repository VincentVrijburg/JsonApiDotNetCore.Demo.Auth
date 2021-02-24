using System;
using System.Threading.Tasks;
using JsonApiDotNetCore.Middleware;
using Microsoft.AspNetCore.Http;

namespace JsonApiDotNetCore.Demo.Auth.Api
{
    internal sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ErrorResponseWriter _errorResponseWriter;

        public ExceptionMiddleware(IExceptionHandler exceptionHandler, RequestDelegate next)
        {
            _next = next;
            _errorResponseWriter = new ErrorResponseWriter(exceptionHandler);
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await _errorResponseWriter.RenderExceptionAsync(exception, httpContext.Response);
            }
        }
    }
}