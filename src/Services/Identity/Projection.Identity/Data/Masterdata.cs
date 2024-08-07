using Projection.Identity.Models;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using ApiResource = Duende.IdentityServer.EntityFramework.Entities.ApiResource;
using ApiScope = Duende.IdentityServer.EntityFramework.Entities.ApiScope;
using Client = Duende.IdentityServer.EntityFramework.Entities.Client;
using IdentityResource = Duende.IdentityServer.EntityFramework.Entities.IdentityResource;
namespace Projection.Identity.Data;

public class MasterData
{
    /// <summary>
    /// This method is to be used in ApplicationDbContext, gets invoked in OnModelCreating override.
    /// Generates migration and keeps track of changes in master data
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void SeedUsingMigration(ModelBuilder modelBuilder)
    {
        DefaultIdentityRoles(modelBuilder);
        DefaultUsers(modelBuilder);
        DefaultUserRoles(modelBuilder);
        DefaultUserClaims(modelBuilder);
    }

    /// <summary>
    /// Creates and saves default users
    /// </summary>
    /// <param name="modelBuilder"></param>
    private static void DefaultUsers(ModelBuilder modelBuilder)
    {
        IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

        List<ApplicationUser> users = new List<ApplicationUser>();

        users.Add(new ApplicationUser(createdDate: new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc))
        {
            FirstName = "SysAdmin",
            LastName = "@Projection",
            DateOfBirth = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            Email = "admin.projection@hotmail.com",
            UserName = "+919888888888",
            PhoneNumber = "+919888888888",
            NormalizedUserName = "+919888888888".ToUpper(),
            NormalizedEmail = "admin.projection@hotmail.com".ToUpper(),
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            PasswordHash = "projection@2023", // This is actual password. DO NOT use this password directly. We need to hash it before storing it.
            SecurityStamp = "124d2334-c5f6-4163-922c-7c6cf18833e1",
            ConcurrencyStamp = "59067e57-3de6-4034-a2b6-5525d5836a63",
            Id = "c0aab6ba-cd71-4010-a9dc-e246997d6183",
        });

        // Hash user passwords for each user
        foreach (var user in users)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);
            // seed user data
            modelBuilder.Entity<ApplicationUser>().HasData(user);
        }
    }

    /// <summary>
    /// Creates Default Identity roles for seeded users
    /// </summary>
    /// <param name="modelBuilder"></param>
    private static void DefaultIdentityRoles(ModelBuilder modelBuilder)
    {
        List<IdentityRole> identityRoles = new List<IdentityRole>();

        // Administrator role
        identityRoles.Add(new IdentityRole
        {
            Id = "117dc0ff-4d12-4a15-8c6b-77e9b903cd63",
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR",
        });

        // Owner role
        identityRoles.Add(new IdentityRole
        {
            Id = "01ce8033-ff7b-49d4-b948-d024771649b9",
            Name = "Owner",
            NormalizedName = "OWNER",
        });

        // seed role data
        foreach (var role in identityRoles)
        {
            modelBuilder.Entity<IdentityRole>().HasData(role);
        }
    }

    /// <summary>
    /// Creates default user roles for seeded users
    /// </summary>
    /// <param name="modelBuilder"></param>
    private static void DefaultUserRoles(ModelBuilder modelBuilder)
    {
        List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();
        userRoles.Add(new IdentityUserRole<string>() { UserId = "c0aab6ba-cd71-4010-a9dc-e246997d6183", RoleId = "117dc0ff-4d12-4a15-8c6b-77e9b903cd63" });

        foreach (var role in userRoles)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(role);
        }
    }

    /// <summary>
    /// Creates default user claims for seeded users
    /// </summary>
    /// <param name="modelBuilder"></param>
    private static void DefaultUserClaims(ModelBuilder modelBuilder)
    {
        List<IdentityUserClaim<string>> userClaims = new()
        {
            // Insert Tenancy Json
            new IdentityUserClaim<string>()
            {
                Id = 1,
                UserId = "c0aab6ba-cd71-4010-a9dc-e246997d6183",
                ClaimType = "TenancyJson",
                ClaimValue = "{\"Id\": \"18ce327c-720e-4c87-b0b1-756eabb37c7b\",\"Name\": \"Tenant1\",\"DefaultConnection\": \"Host=192.168.1.19;Port=5432;Database=Projection.Accounting.Dev.Tenant1;User Id=sa;Password=Radeon1GB#;\"}"
            }
        };

        foreach (var claim in userClaims)
        {
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(claim);
        }
    }

    /// <summary>
    /// This method is to be used in ConfigurationDbContext, gets invoked in OnModelCreating override.
    /// Generates migration and keeps track of changes in Identity Server 4 Configuration
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void SeedIdentityServerClients(ModelBuilder modelBuilder)
    {
        // Get API resources from Data/Configuration/IdentityServer.cs
        var apiResources = Configuration.IdentityServer.GetApiResources();

        // Seed API resources
        foreach (var apiResource in apiResources)
        {
            modelBuilder.Entity<ApiResource>().HasData(apiResource);
        }

        // Get API scopes from Data/Configuration/IdentityServer.cs
        var apiScopes = Configuration.IdentityServer.GetApiScopes();

        // Seed API scopes
        foreach (var apiScope in apiScopes)
        {
            modelBuilder.Entity<ApiScope>().HasData(apiScope);
        }

        // Get API resources scopes from Data/Configuration/IdentityServer.cs
        var apiResourceScopes = Configuration.IdentityServer.GetApiResourceScopes();

        // Seed API resources scopes
        foreach (var apiResourceScope in apiResourceScopes)
        {
            modelBuilder.Entity<ApiResourceScope>().HasData(apiResourceScope);
        }

        // Get Identity resources from Data/Configuration/IdentityServer.cs
        var identityResources = Configuration.IdentityServer.GetIdentityResources();

        // Seed Identity resources
        foreach (var identityResource in identityResources)
        {
            modelBuilder.Entity<IdentityResource>().HasData(identityResource);
        }

        // Get Identity resource claims and seed them from Data/Configuration/IdentityServer.cs
        var identityResourceClaims = Configuration.IdentityServer.GetIdentityResourceClaims();

        // Seed Identity resource claims
        foreach (var identityResourceClaim in identityResourceClaims)
        {
            modelBuilder.Entity<IdentityResourceClaim>().HasData(identityResourceClaim);
        }

        // Get clients from Data/Configuration/IdentityServer.cs
        var clients = Configuration.IdentityServer.GetClients();

        // Seed clients
        foreach (var client in clients)
        {
            modelBuilder.Entity<Client>().HasData(client);
        }

        var clientCorsOrigins = Configuration.IdentityServer.GetClientCorsOrigins();
        foreach (var clientCorsOrigin in clientCorsOrigins)
        {
            modelBuilder.Entity<ClientCorsOrigin>().HasData(clientCorsOrigin);
        }


        var clientScopes = Configuration.IdentityServer.GetClientScopes();
        foreach (var scope in clientScopes)
        {
            modelBuilder.Entity<ClientScope>().HasData(scope);
        }


        var clientGrantTypes = Configuration.IdentityServer.GetClientGrantTypes();
        foreach (var grantType in clientGrantTypes)
        {
            modelBuilder.Entity<ClientGrantType>().HasData(grantType);
        }


        var clientPostLogoutRedirectUris = Configuration.IdentityServer.GetClientPostLogoutRedirectUris();
        foreach (var redirectUri in clientPostLogoutRedirectUris)
        {
            modelBuilder.Entity<ClientPostLogoutRedirectUri>().HasData(redirectUri);
        }

        var clientRedirectUris = Configuration.IdentityServer.GetClientRedirectUris();
        foreach (var uri in clientRedirectUris)
        {
            modelBuilder.Entity<ClientRedirectUri>().HasData(uri);
        }

        var clientSecrets = Configuration.IdentityServer.GetClientSecrets();


        foreach (var secret in clientSecrets)
        {
            modelBuilder.Entity<ClientSecret>().HasData(secret);
        }
    }

}