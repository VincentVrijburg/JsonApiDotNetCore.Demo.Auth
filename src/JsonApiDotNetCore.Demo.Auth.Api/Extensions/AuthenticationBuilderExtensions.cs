using JsonApiDotNetCore.Demo.Auth.Api.Handlers;
using JsonApiDotNetCore.Demo.Auth.Data.Models.Configuration;
using Microsoft.AspNetCore.Authentication;

namespace JsonApiDotNetCore.Demo.Auth.Api.Extensions
{
    public static class AuthenticationBuilderExtensions
    {
        #region AuthenticationBuilder

        public static AuthenticationBuilder AddKey(this AuthenticationBuilder builder)
        {
            builder.AddScheme<KeyAuthenticationOptions, KeyAuthenticationHandler>("Key", null);

            return builder;
        }

        #endregion
    }
}