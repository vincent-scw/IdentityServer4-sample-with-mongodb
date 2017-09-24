using Sample.IdentityServer.MongoDb.Models;

namespace Sample.IdentityServer.MongoDb
{
    public interface IRepository
    {
        MongoDbUser GetUserByUsername(string username);
        MongoDbUser GetUserById(string id);
        bool ValidatePassword(string username, string plainTextPassword, out string userId);
        MongoDbClient GetClient(string clientId);
        bool Signup(string username, string plainTextPassword);
    }
}
