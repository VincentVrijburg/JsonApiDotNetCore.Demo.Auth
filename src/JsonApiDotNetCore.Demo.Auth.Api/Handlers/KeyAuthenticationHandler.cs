using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using JsonApiDotNetCore.Demo.Auth.Api.Extensions;
using JsonApiDotNetCore.Demo.Auth.Api.Services.Abstractions;
using JsonApiDotNetCore.Demo.Auth.Data.Models.Configuration;
using JsonApiDotNetCore.Errors;
using JsonApiDotNetCore.Serialization.Objects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JsonApiDotNetCore.Demo.Auth.Api.Handlers
{
    public class KeyAuthenticationHandler : AuthenticationHandler<KeyAuthenticationOptions>
    {
        private readonly IKeyAuthenticationService _keyAuthenticationService;
        
        public KeyAuthenticationHandler(
            IKeyAuthenticationService keyAuthenticationService,
            IOptionsMonitor<KeyAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _keyAuthenticationService = keyAuthenticationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var apiKey = Request.GetApiKey();
            if (string.IsNullOrEmpty(apiKey))
            {
                return AuthenticateResult.NoResult();
            }

            if (!await _keyAuthenticationService.IsAuthenticatedAsync(apiKey))
            {
                return AuthenticateResult.NoResult();
            }
            
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, _keyAuthenticationService.UserId) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            
            return AuthenticateResult.Success(ticket);
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            throw new JsonApiException(new Error(HttpStatusCode.Unauthorized)
            {
                Title = "Unauthorized",
                Detail = "Invalid API key. Check our documentation for authentication: https://docs.example.com"
            });
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            throw new JsonApiException(new Error(HttpStatusCode.Unauthorized)
            {
                Title = "Forbidden",
                Detail = "Insufficient permissions. Check our documentation for authorization: https://docs.example.com"
            });
        }
    }
}