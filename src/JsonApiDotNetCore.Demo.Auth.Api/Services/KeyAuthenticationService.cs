using System.Threading.Tasks;
using JsonApiDotNetCore.Demo.Auth.Api.Services.Abstractions;
using JsonApiDotNetCore.Demo.Auth.Data.Repository.Abstraction;

namespace JsonApiDotNetCore.Demo.Auth.Api.Services
{
    public class KeyAuthenticationService : IKeyAuthenticationService
    {
        private readonly IKeyRepository _keyRepository;
        
        public string UserId { get; private set; }
        
        public KeyAuthenticationService(IKeyRepository keyRepository)
        {
            _keyRepository = keyRepository;
        }

        public async Task<bool> IsAuthenticatedAsync(string apiKey)
        {
            var key = await _keyRepository.GetByValueAsync(apiKey);
            if (key == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(key.UserId))
            {
                return false;
            }
            
            UserId = key.UserId;
            return true;
        }
    }
}