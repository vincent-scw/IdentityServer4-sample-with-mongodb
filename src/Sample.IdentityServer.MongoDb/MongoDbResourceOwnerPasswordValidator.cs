using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Sample.IdentityServer.MongoDb
{
    public class MongoDbResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IRepository _repository;

        public MongoDbResourceOwnerPasswordValidator(IRepository repository)
        {
            _repository = repository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = _repository.ValidatePassword(context.UserName, context.Password, out string userId) 
                ? new GrantValidationResult(userId, "password", System.DateTime.Now) 
                : new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid_username_or_password");

            return Task.FromResult(0);
        }
    }
}
