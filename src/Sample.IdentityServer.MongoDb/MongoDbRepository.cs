using System;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Sample.IdentityServer.MongoDb.Models;

namespace Sample.IdentityServer.MongoDb
{
    public class MongoDbRepository : IRepository
    {
        private readonly IPasswordHasher<MongoDbUser> _passwordHasher;
        private readonly IMongoDatabase _db;
        private const string UsersCollectionName = "Users";
        private const string ClientsCollectionName = "Clients";
        
        public MongoDbRepository(DbConnectionSettings settings, IPasswordHasher<MongoDbUser> passwordHasher)
        {
            _passwordHasher = passwordHasher;
            var client = new MongoClient(settings.ClientSettings);
            _db = client.GetDatabase(settings.Database);
        }

        public MongoDbUser GetUserByUsername(string username)
        {
            var collection = _db.GetCollection<MongoDbUser>(UsersCollectionName);
            var filter = Builders<MongoDbUser>.Filter.Eq(u => u.Username, username);
            return collection.Find(filter).SingleOrDefaultAsync().Result;
        }

        public MongoDbUser GetUserById(string id)
        {
            var collection = _db.GetCollection<MongoDbUser>(UsersCollectionName);
            var filter = Builders<MongoDbUser>.Filter.Eq(u => u.Id, id);
            return collection.Find(filter).SingleOrDefaultAsync().Result;
        }

        public bool ValidatePassword(string username, string plainTextPassword, out string userId)
        {
            userId = string.Empty;
            var user = GetUserByUsername(username);
            if (user == null)
            {
                return false;
            }

            userId = user.Id;
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, plainTextPassword);
            switch (result)
            {
                case PasswordVerificationResult.Success:
                    return true;
                case PasswordVerificationResult.Failed:
                    return false;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        public MongoDbClient GetClient(string clientId)
        {
            var collection = _db.GetCollection<MongoDbClient>(ClientsCollectionName);
            var filter = Builders<MongoDbClient>.Filter.Eq(x => x.ClientId, clientId);
            return collection.Find(filter).SingleOrDefaultAsync().Result;
        }

        public bool Signup(string username, string plainTextPassword)
        {
            var u = GetUserByUsername(username);
            if (u != null)
                return false;

            var user = new MongoDbUser() { Username = username };
            user.HashedPassword = _passwordHasher.HashPassword(user, plainTextPassword);
            _db.GetCollection<MongoDbUser>(UsersCollectionName).InsertOne(user);

            return true;
        }
    }
}
