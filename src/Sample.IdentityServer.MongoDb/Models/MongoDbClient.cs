using System.Collections.Generic;
using IdentityServer4.Models;
using MongoDB.Bson;

namespace Sample.IdentityServer.MongoDb.Models
{
    public class MongoDbClient : Client // Simplely inherits IdentityServer4.Models.Client
    {
        public ObjectId Id { get; set; }
    }
}
