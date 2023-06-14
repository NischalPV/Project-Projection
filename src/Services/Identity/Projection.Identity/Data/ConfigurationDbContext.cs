using Duende.IdentityServer.EntityFramework.Extensions;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace Projection.Identity.Data;

public class ConfigurationDbContext : Duende.IdentityServer.EntityFramework.DbContexts.ConfigurationDbContext<ConfigurationDbContext>
{
    // private readonly ConfigurationStoreOptions _storeOptions;

    public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options) : base(options)
    {
        // _storeOptions = storeOptions;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.ConfigureClientContext(_storeOptions);
        // modelBuilder.ConfigureResourcesContext(_storeOptions);

        //modelBuilder.HasDefaultSchema(GlobalConstants.DEFAULT_SCHEMA);

        base.OnModelCreating(modelBuilder);

        MasterData.SeedIdentityServerClients(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
