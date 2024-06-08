
using FluentValidation;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.BuildingBlocks.IntegrationEventLogEF.Services;
using Projection.Common.Behaviours;
using Projection.Common.IntegrationService;
using Projection.ServiceDefaults;
using Projection.ServiceDefaults.Services;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Projection.ProcessManagement.Behaviours;
using Projection.ProcessManagement.Infrastructure.Data;
using Projection.ProcessManagement.Features.Masterdata.Commands;
using Projection.ProcessManagement.Features.Masterdata.Commands.Validations;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var Configuration = builder.Configuration;

        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        // Add the authentication services to DI
        builder.AddDefaultAuthentication();

        builder.Services.AddDbContext<ProcessManagementDbContext>((serviceProvider, options) =>
        {
            //var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            //var claims = httpContextAccessor.HttpContext.User.Claims;
            //var tenancyJson = claims.FirstOrDefault(c => c.Type == "TenancyJson")?.Value;

            //if (string.IsNullOrEmpty(tenancyJson))
            //{
            //    throw new Exception("TenancyJson claim is missing");
            //}

            //var tenancy = JsonConvert.DeserializeObject<TenancySettings>(tenancyJson);

            //var connectionString = tenancy.AccountingDbConnection ?? configuration.GetConnectionString("DefaultConnection");

            System.Diagnostics.Debug.WriteLine("ProcessManagementDbContext::conn ->" + connectionString);

            //options.UseNpgsql(connectionString, npgsqlOptionsAction: sqlOptions =>
            //{
            //    sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            //    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            //});

            options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            });

        }, ServiceLifetime.Scoped);

        builder.Services.AddDbContext<IntegrationEventLogContext>((serviceProvider, options) =>
        {
            //var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            //var claims = httpContextAccessor.HttpContext.User.Claims;
            //var tenancyJson = claims.FirstOrDefault(c => c.Type == "TenancyJson")?.Value;

            //if (string.IsNullOrEmpty(tenancyJson))
            //{
            //    throw new Exception("TenancyJson claim is missing");
            //}

            //var tenancy = JsonConvert.DeserializeObject<TenancySettings>(tenancyJson);
            //var connectionString = tenancy.AccountingDbConnection ?? configuration.GetConnectionString("DefaultConnection");

            //options.UseNpgsql(connectionString, npgsqlOptionsAction: sqlOptions =>
            //{
            //    sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            //    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            //});

            options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            });
        });

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Add the integration services that consume the DbContext
        builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<IntegrationEventLogContext>>();

        builder.Services.AddTransient(typeof(IApiIntegrationEventService<>), typeof(ApiIntegrationEventService<>));

        builder.AddRabbitMqEventBus("EventBus", Assembly.GetExecutingAssembly());

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<IIdentityService, IdentityService>();

        // Configure mediatR
        var services = builder.Services;

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        services.AddSingleton<IValidator<ProcessCreateCommand>, ProcessCreateCommandValidator>();

    }
}

