using Sample.IdentityServer.MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.IdentityServer.MongoDb
{
    public interface ILoginService
    {
        bool ValidateCredentials(string username, string password);

        MongoDbUser FindByUsername(string username);

        bool Signup(string username, string password);
    }

    public class LoginService : ILoginService
    {
        private readonly IRepository _repository;

        public LoginService(IRepository repository)
        {
            _repository = repository;
        }

        public bool ValidateCredentials(string username, string password)
        {
            return _repository.ValidatePassword(username, password, out string _);
        }

        public MongoDbUser FindByUsername(string username)
        {
            return _repository.GetUserByUsername(username);
        }

        public bool Signup(string username, string password)
        {
            return _repository.Signup(username, password);
        }
    }
}
