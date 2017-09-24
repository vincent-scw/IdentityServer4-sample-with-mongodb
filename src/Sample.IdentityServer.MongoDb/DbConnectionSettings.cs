using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Sample.IdentityServer.MongoDb.Models;

namespace Sample.IdentityServer.MongoDb
{
    public class DbConnectionSettings
    {
        private readonly IOptions<DbConnectionSettingsModel> _settings;
        public DbConnectionSettings(IOptions<DbConnectionSettingsModel> settings)
        {
            _settings = settings;
        }

        private MongoClientSettings _mongoClientSettings;
        public MongoClientSettings ClientSettings
        {
            get
            {
                if (_mongoClientSettings == null)
                {
                    _mongoClientSettings = new MongoClientSettings
                    {
                        Server = new MongoServerAddress(_settings.Value.Host, _settings.Value.Port),
                        UseSsl = _settings.Value.UseSsL
                    };
                    if (_settings.Value.UseSsL)
                    {
                        _mongoClientSettings.SslSettings = new SslSettings
                        {
                            EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                        };
                    }

                    var identity = new MongoInternalIdentity(_settings.Value.Database, _settings.Value.UserName);
                    var evidence = new PasswordEvidence(_settings.Value.Password);

                    _mongoClientSettings.Credentials = new List<MongoCredential>()
                    {
                        new MongoCredential("SCRAM-SHA-1", identity, evidence)
                    };
                }

                return _mongoClientSettings;
            }
        }

        public string Database => _settings.Value.Database;
    }
}
