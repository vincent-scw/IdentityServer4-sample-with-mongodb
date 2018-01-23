using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

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
                    ClientId = "ApiClient",
                    ClientName = "API Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new List<string> {"WebApi"},
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResource {
                    Name = "roles",
                    UserClaims = new List<string> {"role"}
                },
                new IdentityResource
                {
                    Name = "WebApi"
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource {
                    Name = "WebApi",
                    DisplayName = "Web API",
                    UserClaims = { "role" }
                }
            };
        }
    }
}
