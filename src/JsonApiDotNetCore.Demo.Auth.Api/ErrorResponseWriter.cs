using System;
using System.Threading.Tasks;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Middleware;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace JsonApiDotNetCore.Demo.Auth.Api
{
    internal sealed class ErrorResponseWriter
    {
        private readonly IExceptionHandler _exceptionHandler;
        private readonly IJsonApiOptions _jsonApiOptions;

        public ErrorResponseWriter(IExceptionHandler exceptionHandler, IJsonApiOptions jsonApiOptions)
        {
            _exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
            _jsonApiOptions = jsonApiOptions ?? throw new ArgumentNullException(nameof(jsonApiOptions));
        }

        public async Task RenderExceptionAsync(Exception exception, HttpResponse httpResponse)
        {
            var errorDocument = _exceptionHandler.HandleException(exception);
            string responseContent = JsonConvert.SerializeObject(errorDocument, _jsonApiOptions.SerializerSettings);

            httpResponse.ContentType = HeaderConstants.MediaType;
            httpResponse.StatusCode = (int) errorDocument.GetErrorStatusCode();

            await httpResponse.WriteAsync(responseContent);
            await httpResponse.CompleteAsync();
        }
    }
}
