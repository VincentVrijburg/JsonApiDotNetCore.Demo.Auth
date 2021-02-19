using System.Threading.Tasks;
using JsonApiDotNetCore.Demo.Auth.Data.Models;

namespace JsonApiDotNetCore.Demo.Auth.Data.Repository.Abstraction
{
    public interface IKeyRepository
    {
        public Task<Key> GetByValueAsync(string key);
    }
}