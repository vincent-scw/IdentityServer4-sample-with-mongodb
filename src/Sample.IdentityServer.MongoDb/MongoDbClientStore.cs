using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Sample.IdentityServer.MongoDb
{
    public class MongoDbClientStore : IClientStore
    {
        private readonly IRepository _repository;

        public MongoDbClientStore(IRepository repository)
        {
            _repository = repository;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = _repository.GetClient(clientId);
            if (client == null)
            {
                return Task.FromResult<Client>(null);
            }

            return Task.FromResult((Client) client);
        }
    }
}
