using Projection.Identity.Data;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Projection.Identity.Factories;

public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
{
    public ConfigurationDbContext CreateDbContext(string[] args)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        var storeOptions = new ConfigurationStoreOptions();

        optionsBuilder.UseNpgsql(config["ConnectionStrings:DefaultConnection"], npgsqlOptionsAction: o => o.MigrationsAssembly("Projection.Identity"));
        //optionsBuilder.UseSqlServer(config["ConnectionStrings:DefaultConnection"], sqlServerOptionsAction: o => o.MigrationsAssembly("Projection.Identity"));

        var dbContext = new ConfigurationDbContext(optionsBuilder.Options);
        dbContext.StoreOptions = storeOptions;

        return dbContext;
        //return new ConfigurationDbContext(optionsBuilder.Options);
    }
}