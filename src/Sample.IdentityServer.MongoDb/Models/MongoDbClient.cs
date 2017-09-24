using System.Collections.Generic;
using MongoDB.Bson;

namespace Sample.IdentityServer.MongoDb.Models
{
    public class MongoDbClient
    {
        public ObjectId Id { get; set; }
        public string ClientId { get; set; }
        public List<string> RedirectUris { get; set; } 
        public List<string> ClientSecrets { get; set; }
        public List<string> AllowedScopes { get; set; }
    }
}
