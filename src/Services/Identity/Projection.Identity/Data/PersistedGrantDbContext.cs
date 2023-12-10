using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Projection.Common.GlobalConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Projection.Identity.Data;



public class PersistedGrantDbContext : Duende.IdentityServer.EntityFramework.DbContexts.PersistedGrantDbContext<PersistedGrantDbContext>
{
    // private readonly OperationalStoreOptions _storeOptions;

    public PersistedGrantDbContext(DbContextOptions<PersistedGrantDbContext> options) : base(options)
    {
        // _storeOptions = storeOptions.Value;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.IDENTITY_SCHEMA);
        base.OnModelCreating(modelBuilder);
    }
}
