using System.Threading.Tasks;

namespace JsonApiDotNetCore.Demo.Auth.Api.Services.Abstractions
{
    public interface IKeyAuthenticationService
    {
        string UserId { get; }
        Task<bool> IsAuthenticatedAsync(string apiKey);
    }
}