using System.Linq;
using Microsoft.AspNetCore.Http;

namespace JsonApiDotNetCore.Demo.Auth.Api.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetApiKey(this HttpRequest request)
        {
            if (request.Query.TryGetValue("apiKey", out var query))
            {
                return query.FirstOrDefault();
            }

            if (!request.Headers.TryGetValue("Authorization", out var headerValue))
            {
                return string.Empty;
            }
            
            var value = headerValue.FirstOrDefault();
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
                
            var values = value.Split(' ');
            if (!values[0].ToLower().Equals("bearer"))
            {
                return string.Empty;
            }

            return string.IsNullOrEmpty(values[1]) ? string.Empty : values[1];
        }
    }
}