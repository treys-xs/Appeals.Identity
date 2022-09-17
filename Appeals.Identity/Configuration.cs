using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityModel;

namespace Appeals.Identity
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("AppealsWebApi", "Web Api")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>()
            {
                new ApiResource("AppealsWebApi", "Web Api", new []
                { JwtClaimTypes.Name })
                {
                    Scopes = {"AppealsWebApi"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>()
            {
                new Client()
                {
                    ClientId = "appeals-web-api",
                    ClientName = "Appeals Web Api",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "AppealsWebApi"
                    },
                    
                    RedirectUris = { "https://localhost:44388/signin-oidc" },
                    RequireConsent = false
                }
            };
    }
}
