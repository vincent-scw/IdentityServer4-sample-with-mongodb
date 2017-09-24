using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Sample.IdentityServer.MongoDb
{
    public class MongoDbProfileService : IProfileService
    {
        private readonly IRepository _repository;

        public MongoDbProfileService(IRepository repository)
        {
            _repository = repository;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();

            var user = _repository.GetUserById(subjectId);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, subjectId),
                new Claim(JwtClaimTypes.Name, user.Username),
                new Claim(JwtClaimTypes.NickName, user.NickName),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.EmailVerified, user.EmailVerified.ToString().ToLower(), ClaimValueTypes.Boolean),
                new Claim(JwtClaimTypes.Scope, "WebApi")
            };

            context.IssuedClaims = claims;

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _repository.GetUserById(context.Subject.GetSubjectId());

            context.IsActive = (user != null) && user.IsActive;
            return Task.FromResult(0);
        }
    }
}
