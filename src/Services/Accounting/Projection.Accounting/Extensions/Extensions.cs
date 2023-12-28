using Microsoft.Extensions.Configuration;
using Projection.Accounting.Infrastructure.Data;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.BuildingBlocks.IntegrationEventLogEF.Services;
using Projection.Common.Behaviours;
using Projection.Common.IntegrationService;
using Projection.ServiceDefaults.Services;
using Microsoft.EntityFrameworkCore;
using Projection.Accounting.Behaviours;
using FluentValidation;
using Projection.Accounting.Features.Accounting.Commands;
using Projection.Accounting.Core.Entities;
using Projection.Accounting.Commands;
using Projection.Accounting.Validations;
using Projection.Accounting.Features.Accounting.Commands.Validations;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var Configuration = builder.Configuration;

        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        // Add the authentication services to DI
        builder.AddDefaultAuthentication();

        builder.Services.AddDbContext<AccountingDbContext>((serviceProvider, options) =>
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
            
            System.Diagnostics.Debug.WriteLine("AccountingDbContext::conn ->" + connectionString);

            options.UseNpgsql(connectionString, npgsqlOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
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

            options.UseNpgsql(connectionString, npgsqlOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            });
        });

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Add the integration services that consume the DbContext
        builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<IntegrationEventLogContext>>();

        builder.Services.AddTransient(typeof(IApiIntegrationEventService<>), typeof(ApiIntegrationEventService<>));

        builder.AddRabbitMqEventBus("EventBus");

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

        services.AddSingleton<IValidator<AccountCreateCommand>, AccountCreateCommandValidator>();
        services.AddSingleton<IValidator<AccountUpdateCommand>, AccountUpdateCommandValidator>();
        
        //services.AddSingleton<IValidator<IdentifiedCommand<AccountCreateCommand, Account>>, IdentifiedCommandValidator<AccountCreateCommand, Account>>();
    }
}
