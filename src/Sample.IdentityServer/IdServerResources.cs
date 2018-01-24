using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Sample.IdentityServer
{
    public static class IdServerResources
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ApiClient",
                    ClientName = "API Client for client credentials flow",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"WebApi"},
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    }
                },
                new Client
                {
                    ClientId = "AngularSPA",
                    ClientName = "Angular Single Page Application Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,

                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "WebApi",
                        "roles"
                    }
                },
                new Client
                {
                    ClientId = "AuthCode",
                    ClientName = "For authorization code flow",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {"WebApi"},
                    RedirectUris = { "http://127.0.0.1:44330" },
                    PostLogoutRedirectUris = { "http://127.0.0.1:44330" }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResource
                {
                    Name = "roles",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "WebApi",
                    DisplayName = "Web API",
                    Scopes = {new Scope {Name = "WebApi"}}
                }
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "test_user",
                    Username = "test",
                    Password = "12345"
                }
            };
        }
    }
}
