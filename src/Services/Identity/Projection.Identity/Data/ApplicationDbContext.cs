using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projection.Identity.Models;

namespace Projection.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(GlobalConstants.DEFAULT_SCHEMA);
        base.OnModelCreating(builder);
        MasterData.SeedUsingMigration(builder);
    }
}