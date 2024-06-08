using ApiResource = Duende.IdentityServer.EntityFramework.Entities.ApiResource;
using IdentityResource = Duende.IdentityServer.EntityFramework.Entities.IdentityResource;
using ApiScope = Duende.IdentityServer.EntityFramework.Entities.ApiScope;
using Client = Duende.IdentityServer.EntityFramework.Entities.Client;
using Duende.IdentityServer.EntityFramework.Entities;

namespace Projection.Identity.Data.Configuration;

public class IdentityServer
{
    private static IConfiguration _configuration;

    public IdentityServer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// ApiResources define the APIs in your system
    /// </summary>
    /// <returns>
    /// List of ApiResources
    /// </returns>
    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
            {
                new ApiResource()
                {
                    DisplayName = "API Service",
                    Name = "API",
                    Enabled = true,
                    ShowInDiscoveryDocument = true,
                    Created = new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                    NonEditable = false,
                    Id = 1,
                }
            };
    }

    /// <summary>
    /// Identity resources are data like user id, name or email address of a user
    /// </summary>
    /// <returns>
    /// List of Identity Resources
    /// </returns>
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
            {
                new IdentityResource()
                {
                    Name = "openid",
                    DisplayName = "Your user identifier",
                    Description = "Your user identifier",
                    Enabled = true,
                    Required = true,
                    Emphasize = true,
                    ShowInDiscoveryDocument = true,
                    Created = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                    NonEditable = false,
                    Id = 1,
                },
                new IdentityResource()
                {
                    Name = "profile",
                    DisplayName = "User profile",
                    Description = "Your user profile information (first name, last name, etc.)",
                    Enabled = true,
                    Required = true,
                    Emphasize = true,
                    ShowInDiscoveryDocument = true,
                    Created = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                    NonEditable = false,
                    Id = 2,
                },
                new IdentityResource()
                {
                    Name = "email",
                    DisplayName = "Your email address",
                    Description = "Your email address",
                    Enabled = true,
                    Required = true,
                    Emphasize = true,
                    ShowInDiscoveryDocument = true,
                    Created = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                    NonEditable = false,
                    Id = 3,
                }
            };
    }

    /// <summary>
    /// API Scopes represent the scoped access
    /// </summary>
    /// <returns>List of Api Scopes</returns>
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
            {
                new ApiScope()
                {
                    Name = "AccountingAPI",
                    DisplayName = "Accounting API Scope",
                    Enabled = true,
                    Required = false,
                    Emphasize = false,
                    ShowInDiscoveryDocument = true,
                    Id = 1,
                    Created = new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                },
                new ApiScope()
                {
                    Name = "ProcessAPI",
                    DisplayName = "Process Management API Scope",
                    Enabled = true,
                    Required = false,
                    Emphasize = false,
                    ShowInDiscoveryDocument = true,
                    Id = 2,
                    Created = new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                },
            };
    }

    /// <summary>
    /// Scopes for API Resources
    /// </summary>
    public static IEnumerable<ApiResourceScope> GetApiResourceScopes()
    {
        return new List<ApiResourceScope>
            {
                new ApiResourceScope()
                {
                    Id = 1,
                    ApiResourceId = 1,
                    Scope = "AccountingAPI"
                },
                new ApiResourceScope()
                {
                    Id = 2,
                    ApiResourceId = 1,
                    Scope = "ProcessAPI"
                },
            };
    }

    /// <summary>
    /// Identity Resource Claims mapped with IdenityResources
    /// </summary>
    /// <returns>List of Identity Resource Claims</returns>
    public static IEnumerable<IdentityResourceClaim> GetIdentityResourceClaims()
    {
        return new List<IdentityResourceClaim>
            {
                new IdentityResourceClaim()
                {
                    Id = 1,
                    IdentityResourceId = 1,
                    Type = "sub"
                },
                new IdentityResourceClaim()
                {
                    Id = 2,
                    IdentityResourceId = 2,
                    Type = "given_name"
                },
                new IdentityResourceClaim()
                {
                    Id = 3,
                    IdentityResourceId = 2,
                    Type = "family_name"
                },
                new IdentityResourceClaim()
                {
                    Id = 4,
                    IdentityResourceId = 2,
                    Type = "middle_name"
                },
                new IdentityResourceClaim()
                {
                    Id = 5,
                    IdentityResourceId = 2,
                    Type = "nickname"
                },
                new IdentityResourceClaim()
                {
                    Id = 6,
                    IdentityResourceId = 2,
                    Type = "preferred_username"
                },
                new IdentityResourceClaim()
                {
                    Id = 7,
                    IdentityResourceId = 2,
                    Type = "profile"
                },
                new IdentityResourceClaim()
                {
                    Id = 8,
                    IdentityResourceId = 2,
                    Type = "picture"
                },
                new IdentityResourceClaim()
                {
                    Id = 9,
                    IdentityResourceId = 2,
                    Type = "website"
                },
                new IdentityResourceClaim()
                {
                    Id = 10,
                    IdentityResourceId = 2,
                    Type = "gender"
                },
                new IdentityResourceClaim()
                {
                    Id = 11,
                    IdentityResourceId = 2,
                    Type = "birthdate"
                },
                new IdentityResourceClaim()
                {
                    Id = 12,
                    IdentityResourceId = 2,
                    Type = "zoneinfo"
                },
                new IdentityResourceClaim()
                {
                    Id = 13,
                    IdentityResourceId = 2,
                    Type = "locale"
                },
                new IdentityResourceClaim()
                {
                    Id = 14,
                    IdentityResourceId = 2,
                    Type = "updated_at"
                },
            };
    }

    /// <summary>
    /// Gets configured list of Identity Server 4 clients
    /// </summary>
    /// <returns>List of IdServer4 Clients</returns>
    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
            {
                new Client()
                {
                    Id = 1,
                    Enabled = true,
                    ClientId = "projection-ui",
                    ProtocolType = "oidc",
                    RequireClientSecret = true,
                    ClientName = "Projection Frontend UI OpenId Client",
                    Description = "Projection Frontend UI OpenId Client for Development",
                    ClientUri = $"https://localhost:7140",
                    LogoUri = null,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = false,
                    AllowPlainTextPkce = false,
                    RequireRequestObject = false,
                    AllowAccessTokensViaBrowser = false,
                    FrontChannelLogoutSessionRequired = true,
                    FrontChannelLogoutUri = null,
                    BackChannelLogoutSessionRequired = true,
                    BackChannelLogoutUri = null,
                    AllowOfflineAccess = true,
                    IdentityTokenLifetime = 300,
                    AllowedIdentityTokenSigningAlgorithms = null,
                    AccessTokenLifetime = 3600,
                    AuthorizationCodeLifetime = 300,
                    ConsentLifetime = null,
                    AbsoluteRefreshTokenLifetime = 2592000,
                    SlidingRefreshTokenLifetime = 1296000,
                    RefreshTokenUsage = 1,
                    UpdateAccessTokenClaimsOnRefresh = false,
                    RefreshTokenExpiration = 1,
                    AccessTokenType = 0,
                    EnableLocalLogin = true,
                    IncludeJwtId = true,
                    AlwaysSendClientClaims = false,
                    ClientClaimsPrefix = "client_",
                    PairWiseSubjectSalt = null,
                    Created = new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                    Updated = null,
                    LastAccessed = null,
                    UserSsoLifetime = null,
                    UserCodeType = null,
                    DeviceCodeLifetime = 300,
                    NonEditable = false,
                },

                new Client()
                {
                    Id = 3,
                    Enabled = true,
                    ClientId = "projection-ui--prod",
                    ProtocolType = "oidc",
                    RequireClientSecret = true,
                    ClientName = "Projection Frontend UI OpenId Client",
                    Description = "Projection Frontend UI OpenId Client for Production",
                    ClientUri = $"https://projection.web.azurewebsites.net",
                    LogoUri = null,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AlwaysIncludeUserClaimsInIdToken = false,
                    RequirePkce = true,
                    AllowPlainTextPkce = false,
                    RequireRequestObject = false,
                    AllowAccessTokensViaBrowser = true,
                    FrontChannelLogoutSessionRequired = true,
                    FrontChannelLogoutUri = null,
                    BackChannelLogoutSessionRequired = true,
                    BackChannelLogoutUri = null,
                    AllowOfflineAccess = false,
                    IdentityTokenLifetime = 300,
                    AllowedIdentityTokenSigningAlgorithms = null,
                    AccessTokenLifetime = 3600,
                    AuthorizationCodeLifetime = 300,
                    ConsentLifetime = null,
                    AbsoluteRefreshTokenLifetime = 2592000,
                    SlidingRefreshTokenLifetime = 1296000,
                    RefreshTokenUsage = 1,
                    UpdateAccessTokenClaimsOnRefresh = false,
                    RefreshTokenExpiration = 1,
                    AccessTokenType = 0,
                    EnableLocalLogin = true,
                    IncludeJwtId = true,
                    AlwaysSendClientClaims = false,
                    ClientClaimsPrefix = "client_",
                    PairWiseSubjectSalt = null,
                    Created = new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                    Updated = null,
                    LastAccessed = null,
                    UserSsoLifetime = null,
                    UserCodeType = null,
                    DeviceCodeLifetime = 300,
                    NonEditable = false,
                },

                new Client()
                {
                    Id = 2,
                    Enabled = true,
                    ClientId = "projection-accounting-api",
                    ProtocolType = "oidc",
                    RequireClientSecret = true,
                    ClientName = "Projection Accounting API Swagger UI",
                    Description = "Projection Accounting API Swagger UI for Development",
                    ClientUri = null,
                    LogoUri = null,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AlwaysIncludeUserClaimsInIdToken = false,
                    RequirePkce = false,
                    AllowPlainTextPkce = false,
                    RequireRequestObject = false,
                    AllowAccessTokensViaBrowser = true,
                    FrontChannelLogoutSessionRequired = true,
                    FrontChannelLogoutUri = null,
                    BackChannelLogoutSessionRequired = true,
                    BackChannelLogoutUri = null,
                    AllowOfflineAccess = false,
                    IdentityTokenLifetime = 3600,
                    AllowedIdentityTokenSigningAlgorithms = null,
                    AccessTokenLifetime = 3600,
                    AuthorizationCodeLifetime = 300,
                    ConsentLifetime = null,
                    AbsoluteRefreshTokenLifetime = 2592000,
                    SlidingRefreshTokenLifetime = 1296000,
                    RefreshTokenUsage = 1,
                    UpdateAccessTokenClaimsOnRefresh = false,
                    RefreshTokenExpiration = 1,
                    AccessTokenType = 0,
                    EnableLocalLogin = true,
                    IncludeJwtId = true,
                    AlwaysSendClientClaims = false,
                    ClientClaimsPrefix = "client_",
                    PairWiseSubjectSalt = null,
                    Created = new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                    Updated = null,
                    LastAccessed = null,
                    UserSsoLifetime = null,
                    UserCodeType = null,
                    DeviceCodeLifetime = 300,
                    NonEditable = false,
                },

                new Client()
                {
                    Id = 4,
                    Enabled = true,
                    ClientId = "projection-accounting-api--prod",
                    ProtocolType = "oidc",
                    RequireClientSecret = true,
                    ClientName = "Projection Accounting API Swagger UI",
                    Description = "Projection Accounting API Swagger UI for Production",
                    ClientUri = null,
                    LogoUri = null,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AlwaysIncludeUserClaimsInIdToken = false,
                    RequirePkce = true,
                    AllowPlainTextPkce = false,
                    RequireRequestObject = false,
                    AllowAccessTokensViaBrowser = true,
                    FrontChannelLogoutSessionRequired = true,
                    FrontChannelLogoutUri = null,
                    BackChannelLogoutSessionRequired = true,
                    BackChannelLogoutUri = null,
                    AllowOfflineAccess = false,
                    IdentityTokenLifetime = 300,
                    AllowedIdentityTokenSigningAlgorithms = null,
                    AccessTokenLifetime = 3600,
                    AuthorizationCodeLifetime = 300,
                    ConsentLifetime = null,
                    AbsoluteRefreshTokenLifetime = 2592000,
                    SlidingRefreshTokenLifetime = 1296000,
                    RefreshTokenUsage = 1,
                    UpdateAccessTokenClaimsOnRefresh = false,
                    RefreshTokenExpiration = 1,
                    AccessTokenType = 0,
                    EnableLocalLogin = true,
                    IncludeJwtId = true,
                    AlwaysSendClientClaims = false,
                    ClientClaimsPrefix = "client_",
                    PairWiseSubjectSalt = null,
                    Created = new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                    Updated = null,
                    LastAccessed = null,
                    UserSsoLifetime = null,
                    UserCodeType = null,
                    DeviceCodeLifetime = 300,
                    NonEditable = false,
                },

                new Client()
                {
                    Id = 5,
                    Enabled = true,
                    ClientId = "projection-process-api",
                    ProtocolType = "oidc",
                    RequireClientSecret = true,
                    ClientName = "Projection Process Management API Swagger UI",
                    Description = "Projection Process Management API Swagger UI for Development",
                    ClientUri = null,
                    LogoUri = null,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AlwaysIncludeUserClaimsInIdToken = false,
                    RequirePkce = false,
                    AllowPlainTextPkce = false,
                    RequireRequestObject = false,
                    AllowAccessTokensViaBrowser = true,
                    FrontChannelLogoutSessionRequired = true,
                    FrontChannelLogoutUri = null,
                    BackChannelLogoutSessionRequired = true,
                    BackChannelLogoutUri = null,
                    AllowOfflineAccess = false,
                    IdentityTokenLifetime = 3600,
                    AllowedIdentityTokenSigningAlgorithms = null,
                    AccessTokenLifetime = 3600,
                    AuthorizationCodeLifetime = 300,
                    ConsentLifetime = null,
                    AbsoluteRefreshTokenLifetime = 2592000,
                    SlidingRefreshTokenLifetime = 1296000,
                    RefreshTokenUsage = 1,
                    UpdateAccessTokenClaimsOnRefresh = false,
                    RefreshTokenExpiration = 1,
                    AccessTokenType = 0,
                    EnableLocalLogin = true,
                    IncludeJwtId = true,
                    AlwaysSendClientClaims = false,
                    ClientClaimsPrefix = "client_",
                    PairWiseSubjectSalt = null,
                    Created = new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                    Updated = null,
                    LastAccessed = null,
                    UserSsoLifetime = null,
                    UserCodeType = null,
                    DeviceCodeLifetime = 300,
                    NonEditable = false,
                },

                new Client()
                {
                    Id = 6,
                    Enabled = true,
                    ClientId = "projection-process-api--prod",
                    ProtocolType = "oidc",
                    RequireClientSecret = true,
                    ClientName = "Projection Process Management API Swagger UI",
                    Description = "Projection Process Management API Swagger UI for Production",
                    ClientUri = null,
                    LogoUri = null,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AlwaysIncludeUserClaimsInIdToken = false,
                    RequirePkce = true,
                    AllowPlainTextPkce = false,
                    RequireRequestObject = false,
                    AllowAccessTokensViaBrowser = true,
                    FrontChannelLogoutSessionRequired = true,
                    FrontChannelLogoutUri = null,
                    BackChannelLogoutSessionRequired = true,
                    BackChannelLogoutUri = null,
                    AllowOfflineAccess = false,
                    IdentityTokenLifetime = 300,
                    AllowedIdentityTokenSigningAlgorithms = null,
                    AccessTokenLifetime = 3600,
                    AuthorizationCodeLifetime = 300,
                    ConsentLifetime = null,
                    AbsoluteRefreshTokenLifetime = 2592000,
                    SlidingRefreshTokenLifetime = 1296000,
                    RefreshTokenUsage = 1,
                    UpdateAccessTokenClaimsOnRefresh = false,
                    RefreshTokenExpiration = 1,
                    AccessTokenType = 0,
                    EnableLocalLogin = true,
                    IncludeJwtId = true,
                    AlwaysSendClientClaims = false,
                    ClientClaimsPrefix = "client_",
                    PairWiseSubjectSalt = null,
                    Created = new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(0),
                    Updated = null,
                    LastAccessed = null,
                    UserSsoLifetime = null,
                    UserCodeType = null,
                    DeviceCodeLifetime = 300,
                    NonEditable = false,
                },
            };

    }

    /// <summary>
    /// Adds Cors Origin to Clients
    /// </summary>
    /// <returns>List of ClientCorsOrigin entities</returns>
    public static IEnumerable<ClientCorsOrigin> GetClientCorsOrigins()
    {
        return new List<ClientCorsOrigin>
            {
                new ClientCorsOrigin()
                {
                    Id = 1,
                    ClientId = 1,
                    Origin = $"https://localhost:7140"
                },
                new ClientCorsOrigin()
                {
                    Id = 2,
                    ClientId = 3,
                    Origin = $"https://projection360.web.azurewebsites.net"
                },
                new ClientCorsOrigin()
                {
                    Id = 3,
                    ClientId = 2,
                    Origin = $"http://localhost:6002"
                },
                new ClientCorsOrigin()
                {
                    Id = 4,
                    ClientId = 4,
                    Origin = $"https://projection360-accounting-api.azurewebsites.net"
                },
                new ClientCorsOrigin()
                {
                    Id = 5,
                    ClientId = 5,
                    Origin = $"http://localhost:5078"
                },
                new ClientCorsOrigin()
                {
                    Id = 6,
                    ClientId = 6,
                    Origin = $"https://projection360-process-api.azurewebsites.net"
                },
            };
    }

    /// <summary>
    /// Maps Client Scopes with configured clients
    /// </summary>
    /// <returns>List of ClientScope entities.</returns>
    public static IEnumerable<ClientScope> GetClientScopes()
    {
        return new List<ClientScope>()
            {
                new ClientScope()
                {
                    Id = 1,
                    ClientId = 1,
                    Scope = Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId
                },

                new ClientScope()
                {
                    Id = 2,
                    ClientId = 1,
                    Scope = Duende.IdentityServer.IdentityServerConstants.StandardScopes.Profile
                },
                new ClientScope()
                {
                    Id = 5,
                    ClientId = 3,
                    Scope = Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId
                },

                new ClientScope()
                {
                    Id = 6,
                    ClientId = 3,
                    Scope = Duende.IdentityServer.IdentityServerConstants.StandardScopes.Profile
                },
                new ClientScope()
                {
                    Id = 3,
                    ClientId = 1,
                    Scope = "AccountingAPI"
                },
                new ClientScope()
                {
                    Id = 4,
                    ClientId = 2,
                    Scope = "AccountingAPI"
                },
                new ClientScope()
                {
                    Id = 7,
                    ClientId = 3,
                    Scope = "AccountingAPI"
                },
                new ClientScope()
                {
                    Id = 8,
                    ClientId = 4,
                    Scope = "AccountingAPI"
                },
                new ClientScope()
                {
                    Id = 9,
                    ClientId = 1,
                    Scope = Duende.IdentityServer.IdentityServerConstants.StandardScopes.OfflineAccess
                },
                new ClientScope()
                {
                    Id = 10,
                    ClientId = 1,
                    Scope = "ProcessAPI"
                },
                new ClientScope()
                {
                    Id = 11,
                    ClientId = 3,
                    Scope = "ProcessAPI"
                },
                new ClientScope()
                {
                    Id = 12,
                    ClientId = 5,
                    Scope = "ProcessAPI"
                },
                new ClientScope()
                {
                    Id = 13,
                    ClientId = 6,
                    Scope = "ProcessAPI"
                },
            };
    }

    /// <summary>
    /// Adds Grant Types to clients
    /// </summary>
    /// <returns>List of ClientGrantType entities.</returns>
    public static IEnumerable<ClientGrantType> GetClientGrantTypes()
    {
        return new List<ClientGrantType>
            {
                new ClientGrantType()
                {
                    Id = 1,
                    ClientId = 1,
                    GrantType = "authorization_code"
                },
                new ClientGrantType()
                {
                    Id = 2,
                    ClientId = 2,
                    GrantType = "implicit"
                },
                new ClientGrantType()
                {
                    Id = 3,
                    ClientId = 3,
                    GrantType = "implicit"
                },
                new ClientGrantType()
                {
                    Id = 4,
                    ClientId = 4,
                    GrantType = "implicit"
                },
                new ClientGrantType()
                {
                    Id = 5,
                    ClientId = 5,
                    GrantType = "implicit"
                },
                new ClientGrantType()
                {
                    Id = 6,
                    ClientId = 6,
                    GrantType = "implicit"
                },
            };
    }

    /// <summary>
    /// Adds Post RedirectUri to Clients
    /// </summary>
    /// <returns>List of ClientPostLogoutRedirectUri entities</returns>
    public static IEnumerable<ClientPostLogoutRedirectUri> GetClientPostLogoutRedirectUris()
    {
        return new List<ClientPostLogoutRedirectUri>
            {
                new ClientPostLogoutRedirectUri()
                {
                    Id = 1,
                    ClientId = 1,
                    PostLogoutRedirectUri = $"https://localhost:7140/signout-callback-oidc"
                },
                new ClientPostLogoutRedirectUri()
                {
                    Id = 2,
                    ClientId = 2,
                    PostLogoutRedirectUri = $"http://localhost:6002/swagger/"
                },
                new ClientPostLogoutRedirectUri()
                {
                    Id = 3,
                    ClientId = 3,
                    PostLogoutRedirectUri = $"https://projection-web.azurewebsites.net/"
                },
                new ClientPostLogoutRedirectUri()
                {
                    Id = 4,
                    ClientId = 4,
                    PostLogoutRedirectUri = $"https://projection-accounting-api.azurewebsites.net/swagger/"
                },
                new ClientPostLogoutRedirectUri()
                {
                    Id = 5,
                    ClientId = 5,
                    PostLogoutRedirectUri = $"http://localhost:5078/swagger/"
                },
                new ClientPostLogoutRedirectUri()
                {
                    Id = 6,
                    ClientId = 6,
                    PostLogoutRedirectUri = $"https://projection360-process-api.azurewebsites.net/swagger/"
                },

            };
    }

    /// <summary>
    /// Adds Client RedirectUri to Clients
    /// </summary>
    /// <returns>List of ClientRedirectUri entities</returns>
    public static IEnumerable<ClientRedirectUri> GetClientRedirectUris()
    {
        return new List<ClientRedirectUri>
            {
                new ClientRedirectUri()
                {
                    Id = 1,
                    ClientId = 1,
                    RedirectUri = $"https://localhost:7140/signin-oidc"
                },
                new ClientRedirectUri()
                {
                    Id = 2,
                    ClientId = 2,
                    RedirectUri = $"http://localhost:6002/swagger/oauth2-redirect.html"
                },
                new ClientRedirectUri()
                {
                    Id = 3,
                    ClientId = 3,
                    RedirectUri = $"https://projection360-web.azurewebsites.net/"
                },
                new ClientRedirectUri()
                {
                    Id = 4,
                    ClientId = 4,
                    RedirectUri = $"https://projection360-accounting-api.azurewebsites.net/swagger/oauth2-redirect.html"
                },
                new ClientRedirectUri()
                {
                    Id = 5,
                    ClientId = 5,
                    RedirectUri = $"http://localhost:5078/swagger/oauth2-redirect.html"
                },
                new ClientRedirectUri()
                {
                    Id = 6,
                    ClientId = 6,
                    RedirectUri = $"https://projection360-process-api.azurewebsites.net/swagger/oauth2-redirect.html"
                },
            };

    }

    public static IEnumerable<ClientSecret> GetClientSecrets()
    {
        return new List<ClientSecret>
        {
            new ClientSecret()
            {
                Id = 1,
                ClientId = 1,
                Value = "projection@2023".Sha256(),
            },
            new ClientSecret()
            {
                Id = 2,
                ClientId = 2,
                Value = "projection@2023".Sha256(),
            },
            new ClientSecret()
            {
                Id = 4,
                ClientId = 4,
                Value = "projection@2023".Sha256(),
            }
        };
    }
}
