using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonApiDotNetCore.Demo.Auth.Data.Models;
using JsonApiDotNetCore.Demo.Auth.Data.Repository.Abstraction;

namespace JsonApiDotNetCore.Demo.Auth.Data.Repository
{
    public class KeyRepository : IKeyRepository
    {
        private readonly IReadOnlyCollection<Key> _collection = new[]
        {
            new Key
            {
                UserId = "d258cd32-a61e-466b-a39c-7630c656f5b6",
                Value = "HpjtmVYExiltL1lmtGbvqUr0mUQ1Ngke"
            },
            new Key
            {
                UserId = "4e74c44c-5e28-4d9d-89ca-cbf9051da4c6",
                Value = "pTXohlfeqMCWxyTqtfNqb6QeeAWgdEpV"
            }
        };
        
        public Task<Key> GetByValueAsync(string key)
        {
            return Task.FromResult(_collection.FirstOrDefault(k => k.Value == key));
        }
    }
}