using System;
using System.Threading.Tasks;
using JsonApiDotNetCore.Middleware;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace JsonApiDotNetCore.Demo.Auth.Api
{
    internal sealed class ErrorResponseWriter
    {
        private readonly IExceptionHandler _exceptionHandler;

        public ErrorResponseWriter(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
        }

        public async Task RenderExceptionAsync(Exception exception, HttpResponse httpResponse)
        {
            var errorDocument = _exceptionHandler.HandleException(exception);
            string responseContent = JsonConvert.SerializeObject(errorDocument);

            httpResponse.ContentType = HeaderConstants.MediaType;
            httpResponse.StatusCode = (int) errorDocument.GetErrorStatusCode();

            await httpResponse.WriteAsync(responseContent);
            await httpResponse.CompleteAsync();
        }
    }
}
